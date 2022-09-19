app.controller('ProgrammeCtrl', function ($scope, $localStorage, $window, $filter, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Programme";
    $scope.showDefineBtn = false;
    /*Reset Programme*/
    $scope.resetProgramme = function () {
        $scope.Programme = {};
    };

    //Steffi Code Starts
    $scope.ProgPartTable = true;
    $scope.IsVisibleDeleteBtn = false;
    $scope.ShowDeleteBtn = function () {
        $scope.IsVisibleDeleteBtn = $scope.ShowDelete;
    }

    $scope.editContact = function (data) {
      
        $scope.editing = true;       
        //$scope.TotalParts = angular.copy(data);
    };

    $scope.saveContact = function () {      
        $scope.editing = false;
        //$scope.Value[id] = angular.copy($scope.TotalParts);
        $scope.EditMatrix();
    };
 
    $scope.NewUp = function () {
        $scope.showtableEdit = true;       
    }
    $scope.ShowEditPage = function () {
        $scope.showFormFlag = false;
        $scope.showtableValue = false;
        $scope.showtableEdit = false;
        $scope.Backbtn = false;
        $scope.ProgPartTable = true;
        $scope.getProgrammePartList();

    };

    $scope.MakeMatrix = function () {
       
        $scope.showtable = false;
        if ($scope.Programme.TotalPartsList == null || $scope.Programme.TotalPartsList == undefined) {

           var TotalParts = new Array();
            for (var i = 1; i <= $scope.Programme.TotalParts; i++) {
                var partsObj = {};
                partsObj["Index"] = i;
                partsObj["PartName"] = null;
                partsObj["PartShortName"] = null;
                partsObj["ExamPatternId"] = null;
                partsObj["TotalNoOfDuration"] = null;
                partsObj["SequenceNo"] = null;
                partsObj["NoOfTerms"] = null;

                TotalParts.push(partsObj);

            }
            $scope.Programme.TotalPartsList = TotalParts;
            $scope.NewUp();
          

        }
        else {


            $scope.showtableValue = true;
        }



    };

    

    $scope.AddMatrix = function () {

        var validationFlag = true;
        var validationTotalFlag = true;
        var validationTermsFlag = true;
        var validationSameValue = true;
        var vali = true;
        var total = 0;
        var totParts = 0;
        var ProgDura = 0;

        
        
        //for (var i in $scope.Programme.TotalPartsList)
        for (let i = 0; i < $scope.Programme.TotalPartsList.length; i++) {
            if ($scope.Programme.TotalPartsList[i].PartName == null || $scope.Programme.TotalPartsList[i].PartName == "" || $scope.Programme.TotalPartsList[i].PartName == undefined ||
                $scope.Programme.TotalPartsList[i].PartShortName == null || $scope.Programme.TotalPartsList[i].PartShortName == "" || $scope.Programme.TotalPartsList[i].PartShortName == undefined ||
                $scope.Programme.TotalPartsList[i].ExamPatternId == null || $scope.Programme.TotalPartsList[i].ExamPatternId == "" || $scope.Programme.TotalPartsList[i].ExamPatternId == undefined ||
                $scope.Programme.TotalPartsList[i].TotalNoOfDuration == null || $scope.Programme.TotalPartsList[i].TotalNoOfDuration == "" || $scope.Programme.TotalPartsList[i].TotalNoOfDuration == undefined ||
                $scope.Programme.TotalPartsList[i].SequenceNo == null || $scope.Programme.TotalPartsList[i].SequenceNo == "" || $scope.Programme.TotalPartsList[i].SequenceNo == undefined ||
                $scope.Programme.TotalPartsList[i].NoOfTerms == null || $scope.Programme.TotalPartsList[i].NoOfTerms == "" || $scope.Programme.TotalPartsList[i].NoOfTerms == undefined



            ) {

                validationFlag = false;
                vali = false;

            }

        }
        if (validationFlag == false) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#MarksTemplateConfig')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Save...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        for (var i in $scope.Programme.TotalPartsList) {
            //total += parseInt($scope.Programme.TotalPartsList[i].TotalNoOfDuration, 10);
            ProgDura = parseInt($scope.Programme.ProgrammeDuration, 10);
            total += $scope.Programme.TotalPartsList[i].TotalNoOfDuration;
            //scope.total = total;
            $scope.totParts = $scope.Programme.TotalParts;
            if (ProgDura != total) {
                validationTotalFlag = false;
                validationFlag = false;


            }
            else {
                validationFlag = true;
                validationTotalFlag = true;

            }

        }
        for (var i in $scope.Programme.TotalPartsList) {
            $scope.totParts = $scope.Programme.TotalParts;

            if ($scope.Programme.TotalPartsList[i].SequenceNo > $scope.totParts || $scope.Programme.TotalPartsList[i].SequenceNo < 0) {
                validationTermsFlag = false;
                validationFlag = false;
            }

        }
        for (let i = 0; i < $scope.Programme.TotalPartsList.length; i++) {
            $scope.a = $scope.Programme.TotalPartsList[i].SequenceNo;
            for (let j = i + 1; j < $scope.Programme.TotalPartsList.length; j++) {
                if ($scope.a == $scope.Programme.TotalPartsList[j].SequenceNo) {
                    
                    validationSameValue = false;
                    validationFlag = false;
                }
            }
        }
        
        if (validationTermsFlag == false && vali==true) {
            alert('Your Sequence No should not greater than Total Parts');

        }
        if (validationSameValue == false && vali == true) { 
            alert('Sequence Should not Be Unique');

        }
        if (validationTotalFlag == false && vali == true) {

            alert('Total Duration is not matched with Programme Duration(In Months) !');
        }





        if (validationFlag == true) {

            $http({
                method: 'POST',
                url: 'api/MstProgramme/MstProgrammePartDynamicallyAdd',
                data: $scope.Programme,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        alert(response.obj);
                    }
                    else {
                      
                        alert(response.obj);
                        $scope.showtable = false;
                        $scope.showFormFlag = false;
                        $scope.showtableEdit = false;
                        $state.go('ProgrammeEdit');
                        $scope.ProgPartTable = true;
                        $scope.getProgrammePartList();


                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }

    };

    $scope.EditMatrix = function () {
        
        var validationFlag = true;
        var validationTotalFlag = true;
        var validationTermsFlag = true;
        var validationSameValue = true;
        var vali = true;
        var total = 0;
        var totParts = 0;
        var ProgDura = 0;



        //for (var i in $scope.Programme.TotalPartsList)
        for (let i = 0; i < $scope.Programme.TotalPartsList.length; i++) {
            if ($scope.Programme.TotalPartsList[i].PartName == null || $scope.Programme.TotalPartsList[i].PartName == "" || $scope.Programme.TotalPartsList[i].PartName == undefined ||
                $scope.Programme.TotalPartsList[i].PartShortName == null || $scope.Programme.TotalPartsList[i].PartShortName == "" || $scope.Programme.TotalPartsList[i].PartShortName == undefined ||
                $scope.Programme.TotalPartsList[i].ExamPatternId == null || $scope.Programme.TotalPartsList[i].ExamPatternId == "" || $scope.Programme.TotalPartsList[i].ExamPatternId == undefined ||
                $scope.Programme.TotalPartsList[i].TotalNoOfDuration == null || $scope.Programme.TotalPartsList[i].TotalNoOfDuration == "" || $scope.Programme.TotalPartsList[i].TotalNoOfDuration == undefined ||
                $scope.Programme.TotalPartsList[i].SequenceNo == null || $scope.Programme.TotalPartsList[i].SequenceNo == "" || $scope.Programme.TotalPartsList[i].SequenceNo == undefined ||
                $scope.Programme.TotalPartsList[i].NoOfTerms == null || $scope.Programme.TotalPartsList[i].NoOfTerms == "" || $scope.Programme.TotalPartsList[i].NoOfTerms == undefined



            ) {

                validationFlag = false;
                vali = false;

            }

        }
        if (validationFlag == false) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#MarksTemplateConfig')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Save...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        for (var i in $scope.Programme.TotalPartsList) {
        
            //total += parseInt($scope.Programme.TotalPartsList[i].TotalNoOfDuration, 10);
            ProgDura = parseInt($scope.Programme.ProgrammeDuration, 10);
            total += $scope.Programme.TotalPartsList[i].TotalNoOfDuration;
            //scope.total = total;
            $scope.totParts = $scope.Programme.TotalParts;
            if (ProgDura != total) {
                validationTotalFlag = false;
                validationFlag = false;
               


            }
            else {
                validationFlag = true;
                validationTotalFlag = true;

            }

        }
        for (var i in $scope.Programme.TotalPartsList) {
            $scope.totParts = $scope.Programme.TotalParts;

            if ($scope.Programme.TotalPartsList[i].SequenceNo > $scope.totParts || $scope.Programme.TotalPartsList[i].SequenceNo < 0) {
                validationTermsFlag = false;
                validationFlag = false;
            }

        }
        for (let i = 0; i < $scope.Programme.TotalPartsList.length; i++) {
            $scope.a = $scope.Programme.TotalPartsList[i].SequenceNo;
            for (let j = i + 1; j < $scope.Programme.TotalPartsList.length; j++) {
                if ($scope.a == $scope.Programme.TotalPartsList[j].SequenceNo) {

                    validationSameValue = false;
                    validationFlag = false;
                }
            }
        }

        if (validationTermsFlag == false && vali == true) {
            alert('Your Sequence No should not greater than Total Parts');

        }
        if (validationSameValue == false && vali == true) {
            alert('Sequence Should Be Unique');

        }
        if (validationTotalFlag == false && vali == true) {

            alert('Total Duration is not matched with Programme Duration(In Months) !');
        }

        if (validationFlag == true) {

            $http({
                method: 'POST',
                url: 'api/MstProgramme/MstProgrammePartDynamicallyEdit',
                data: $scope.Programme,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getProgrammePartList();
                        $scope.Backbtn = false;
                       

                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }

     




    };

    $scope.DeleteMatrix = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {

            $scope.Programme = data;

            $http({
                method: 'POST',
                url: 'api/MstProgramme/MstProgrammePartDynamicallyDelete',
                data: $scope.Programme,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {

                        alert(response.obj);
                        $scope.ShowDelete = {};
                        $scope.IsDisabledProgDuration = false;
                        $scope.IsDisabledTotalParts = false;
                        $scope.IsVisibleDeleteBtn = false;
                        $scope.showtableValue = false;
                        $scope.Programme.TotalParts = null;
                        $scope.Programme.TotalPartsList = null;
                        
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }, function () {
            $scope.status = 'You decided not to delete your data.';
            alert($scope.status);
        });
    };

    /*Get Examination Pattern List*/
    $scope.getExamPatternList = function () {

        $http({
            method: 'GET',
            url: 'api/MstExaminationPattern/MstExaminationPatternGetForDropDown',
            //data: $scope.ProgPart,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamPatternList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };


    /*Modify Programme Data*/
    $scope.IsDisabledProgDuration = false;
    $scope.IsDisabledTotalParts = false;
    $scope.modifyProgrammeData = function (data) {
        $scope.Programme = data;
        if ($scope.Programme.TotalPartsList == null || $scope.Programme.TotalPartsList == undefined || $scope.Programme.TotalPartsList == 0) {
           
            $scope.MakeMatrix();
            $scope.IsDisabledProgDuration = false;
            $scope.IsDisabledTotalParts = false;
            $scope.showFormFlag = true;
            $scope.ProgPartTable = false;
            $scope.Backbtn = true;
        }
        else {
            $scope.MakeMatrix();
            $scope.IsDisabledProgDuration = true;
            $scope.IsDisabledTotalParts = true;          
            $scope.showFormFlag = true;
            $scope.ProgPartTable = false;
            $scope.Backbtn = true;

        }



    };

    $scope.getProgrammePartList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstProgramme/MstProgrammePartListGet',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.ProgTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.ProgrammeData = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    //Steffi Code Ends


/*Get Faculty List*/
    $scope.getFacultyList = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetForDropDown',
            //data: $scope.Faculty,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Add Programme*/
    $scope.addProgramme = function () {
       
        /*if (parseInt($scope.Programme.MaxMarks) < parseInt($scope.Programme.MinMarks)) {
            alert("Minimum Marks are not greater than Maximum Marks");
        }
        if (parseInt($scope.Programme.MaxCredits) < parseInt($scope.Programme.MinCredits)) {
            alert("Minimum Credits are not greater than Maximum Credits");
        }*/

        if ($scope.Programme.ProgrammeName == null || $scope.Programme.ProgrammeName === undefined
            || $scope.Programme.FacultyId == null || $scope.Programme.FacultyId === undefined
            || $scope.Programme.ProgrammeCode == null || $scope.Programme.ProgrammeCode === undefined
            || $scope.Programme.ProgrammeDescription == null || $scope.Programme.ProgrammeDescription === undefined
            || $scope.Programme.ProgrammeLevelId == null || $scope.Programme.ProgrammeLevelId === undefined
            || $scope.Programme.ProgrammeModeId == null || $scope.Programme.ProgrammeModeId === undefined
            || $scope.Programme.ProgrammeTypeId == null || $scope.Programme.ProgrammeTypeId === undefined
            || $scope.Programme.EvaluationId == null || $scope.Programme.EvaluationId === undefined
            || $scope.Programme.InstructionMediumId == null || $scope.Programme.InstructionMediumId === undefined
            || $scope.Programme.IsCBCS == null || $scope.Programme.IsCBCS === undefined
            //|| $scope.Programme.MaxMarks == null || $scope.Programme.MaxMarks === undefined
            //|| $scope.Programme.MinMarks == null || $scope.Programme.MinMarks === undefined
            //|| $scope.Programme.IsSepartePassingHead == null || $scope.Programme.IsSepartePassingHead === undefined            
            || $scope.Programme.ProgrammeDuration == null || $scope.Programme.ProgrammeDuration === undefined
            || $scope.Programme.ProgrammeValidity == null || $scope.Programme.ProgrammeValidity === undefined
            || $scope.Programme.TotalParts == null || $scope.Programme.TotalParts === undefined
        ) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please complete the form before Click...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if (parseInt($scope.Programme.MaxMarks) < parseInt($scope.Programme.MinMarks)) {
            alert("Minimum Marks are not greater than Maximum Marks");
        }
        else if (parseInt($scope.Programme.MaxCredits) < parseInt($scope.Programme.MinCredits)) {
            alert("Minimum Credits are not greater than Maximum Credits");
        }
        else {
            $http({
                method: 'POST',
                url: 'api/MstProgramme/MstProgrammeAdd',
                data: $scope.Programme,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        //alert(response.obj.Item2);
                        //$scope.Programme = {};
                       //$scope.getProgrammeList();

                        $scope.MakeMatrix();
                        $scope.Programme.Id = response.obj.Item2;
                        alert(response.obj.Item1);
                        $scope.getProgrammePartList();
                        $scope.showDefineBtn = true;
                        $scope.showtable = true;

                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

  


    /*Update Programme*/
    $scope.editProgramme = function () {
     

        if ($scope.Programme.ProgrammeName == null || $scope.Programme.ProgrammeName === undefined
            || $scope.Programme.FacultyId == null || $scope.Programme.FacultyId === undefined
            || $scope.Programme.ProgrammeCode == null || $scope.Programme.ProgrammeCode === undefined
            || $scope.Programme.ProgrammeDescription == null || $scope.Programme.ProgrammeDescription === undefined
            || $scope.Programme.ProgrammeLevelId == null || $scope.Programme.ProgrammeLevelId === undefined
            || $scope.Programme.ProgrammeModeId == null || $scope.Programme.ProgrammeModeId === undefined
            || $scope.Programme.ProgrammeTypeId == null || $scope.Programme.ProgrammeTypeId === undefined
            || $scope.Programme.EvaluationId == null || $scope.Programme.EvaluationId === undefined
            || $scope.Programme.InstructionMediumId == null || $scope.Programme.InstructionMediumId === undefined
            || $scope.Programme.IsCBCS == null || $scope.Programme.IsCBCS === undefined
            //|| $scope.Programme.MaxMarks == null || $scope.Programme.MaxMarks === undefined
            //|| $scope.Programme.MinMarks == null || $scope.Programme.MinMarks === undefined
            //|| $scope.Programme.IsSepartePassingHead == null || $scope.Programme.IsSepartePassingHead === undefined
            || $scope.Programme.ProgrammeDuration == null || $scope.Programme.ProgrammeDuration === undefined
            || $scope.Programme.ProgrammeValidity == null || $scope.Programme.ProgrammeValidity === undefined
            || $scope.Programme.TotalParts == null || $scope.Programme.TotalParts === undefined
        ) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please complete the form before Click...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
           
        }
        else if (parseInt($scope.Programme.MaxMarks) < parseInt($scope.Programme.MinMarks)) {
            alert("Minimum Marks are not greater than Maximum Marks");
        }
        else if (parseInt($scope.Programme.MaxCredits) < parseInt($scope.Programme.MinCredits)) {
            alert("Minimum Credits are not greater than Maximum Credits");
        }
       else {

            $http({
                method: 'POST',
                url: 'api/MstProgramme/MstProgrammeUpdate',
                data: $scope.Programme,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.Programme.TotalPartsList = null;
                        $scope.MakeMatrix();
                        $scope.showtable = true;
                     
                       // $scope.Programme = {};
                        //$scope.showFormFlag = false;
                        //$scope.getProgrammeList();
                       

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
             }
    };

    /*Delete Programme*/
    $scope.deleteProgramme = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {

            $scope.Programme = data;

            $http({
                method: 'POST',
                url: 'api/MstProgramme/MstProgrammeDelete',
                data: $scope.Programme,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                  
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                       
                        $scope.getProgrammeList();
                        alert(response.obj);
                        
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }, function () {
            $scope.status = 'You decided not to delete your data.';
            alert($scope.status);
        });
    };

    /*Define Programme Part Using LocalStorage*/
    $scope.defineProgrammePart = function () {
        $localStorage.define = {};
        $localStorage.define.faculty = $scope.Programme.FacultyId;
        $localStorage.define.ProgrammeId = $scope.Programme.Id;
        $state.go('ProgrammePartAdd');

    };
    /*Get Programme List*/
    $scope.getProgrammeList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstProgramme/MstProgrammeListGet',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.ProgTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.ProgrammeData = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Enable Programme*/
    $scope.showProgramme = function (data) {

        $scope.newProgramme = data;

        $http({
            method: 'POST',
            url: 'api/MstProgramme/MstProgrammeIsActiveEnable',
            data: $scope.newProgramme,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getProgrammeList();

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    /*Active Disable Programme*/
    $scope.hideProgramme = function (data) {

        $scope.newProgramme = data;

        $http({
            method: 'POST',
            url: 'api/MstProgramme/MstProgrammeIsActiveDisable',
            data: $scope.newProgramme,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getProgrammeList();

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    /*Get ProgrammeLevel List*/
    $scope.getProgrammeLevelList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgrammeLevel/MstProgrammeLevelListGetForDropDown',
            //data: $scope.ProgLevel,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgLevelList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get ProgrammeMode List*/
    $scope.getProgrammeModeList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgrammeMode/MstProgrammeModeListGetForDropDown',
            //data: $scope.ProgMode,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgModeList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get ProgrammeType List*/
    $scope.getProgrammeTypeList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgrammeType/MstProgrammeTypeListGetForDropDown',
            //data: $scope.ProgType,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgTypeList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Evaluation List*/
    $scope.getEvaluationList = function () {

        $http({
            method: 'POST',
            url: 'api/MstEvaluation/EvaluationGetForDropDown',
            //data: $scope.ProgEval,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.EvalList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Instruction Medium List*/
    $scope.getInstMediumList = function () {

        $http({
            method: 'GET',
            url: 'api/InstructionMedium/InstructionMediumGetForDropDown',
            //data: $scope.InstrMedium,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.MediumList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Add New Programme*/
    $scope.newProgrammeAdd = function () {
        $state.go('ProgrammeAdd');
    };

    /*Back to Edit Page of Programme*/
    $scope.backToList = function () {
        $localStorage.define = {};
        $state.go('ProgrammeEdit');
    };
    
    /*Display Programme Data*/
    $scope.displayMstProgramme = function (data) {
        $scope.Programme = data;
    };

    $scope.exportDataofProgramme = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ProgrammeData_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Programme List | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'ProgrammeCode', title: 'Programme Code' },
                { columnid: 'ProgrammeDescription', title: 'Programme Description' },
                { columnid: 'ProgrammeTypeName', title: 'Programme Type Name' },
                { columnid: 'InstructionMediumName', title: 'Instruction Medium Name' },
                { columnid: 'EvaluationName', title: 'Evaluation Name' },
                { columnid: 'IsSeparatePassingHeadSts', title: 'Separate Passing Head Status' },
                { columnid: 'ProgrammeDuration', title: 'Programme Duration' },
                { columnid: 'ProgrammeValidity', title: 'Programme Validity' },
                { columnid: 'TotalParts', title: 'Total Parts' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ProgrammeData]);
    };
});

app.directive('allowPattern', [allowPatternDirective]);

function allowPatternDirective() {
    return {
        restrict: "A",
        compile: function (tElement, tAttrs) {
            return function (scope, element, attrs) {

                element.bind("keypress", function (event) {
                    var keyCode = event.which || event.keyCode;
                    var keyCodeChar = String.fromCharCode(keyCode);

                    if (!keyCodeChar.match(new RegExp(attrs.allowPattern, "i"))) {
                        event.preventDefault();
                        return false;
                    }

                });
            };
        }
    };
}