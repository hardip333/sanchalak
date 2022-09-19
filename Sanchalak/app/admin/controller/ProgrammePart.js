app.controller('ProgrammePart', function ($scope, $localStorage, $filter,$http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    
    $rootScope.pageTitle = "Manage Programme Part";
    $scope.showDefineBtn = false;

    /*Get Proogramme Part List*/
    $scope.getProgramPartList = function () {

        var data = new Object();

        $http({
            method: 'GET',
            url: 'api/MstProgrammePart/MstProgrammePartGet',
           
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
                    $scope.ProgPartParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.ProgrammePartData = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    
    /*Check LocalStorage for Data*/
    if ($localStorage.define) {
        $scope.ProgPart = {};
        $scope.ProgPart.FacultyId = $localStorage.define.faculty;
        $scope.ProgPart.ProgrammeId = $localStorage.define.ProgrammeId;
        //alert($scope.ProgPart.ProgrammeId);
    }

    /*Add Programme Part*/
    $scope.addProgPart = function () {

        if ($scope.ProgPart.FacultyId == null || $scope.ProgPart.FacultyId === undefined
            || $scope.ProgPart.ProgrammeId == null || $scope.ProgPart.ProgrammeId === undefined
            || $scope.ProgPart.ExamPatternId == null || $scope.ProgPart.ExamPatternId === undefined
            || $scope.ProgPart.PartName == null || $scope.ProgPart.PartName === undefined
            || $scope.ProgPart.PartShortName == null || $scope.ProgPart.PartShortName === undefined
            || $scope.ProgPart.SequenceNo == null || $scope.ProgPart.SequenceNo === undefined
            || $scope.ProgPart.NoOfTerms == null || $scope.ProgPart.NoOfTerms === undefined) {

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
        else {
            $http({
                method: 'POST',
                url: 'api/MstProgrammePart/MstProgrammePartAdd',
                data: $scope.ProgPart,
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
                        //alert(response.obj);
                        //$scope.ProgPart = {};
                        $scope.ProgPart.Id = response.obj.Item2;
                        $scope.getProgramPartList();
                        alert(response.obj.Item1);
                        //alert(response.obj.Item2);
                        $scope.showDefineBtn = true;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Define Programme Part Term using LocalStorage*/
    $scope.defineProgrammePartTerm = function () {
        $localStorage.define = {};
        $localStorage.define.faculty = $scope.ProgPart.FacultyId;
        $localStorage.define.ProgrammeId = $scope.ProgPart.ProgrammeId;
        $localStorage.define.ProgrammePartId = $scope.ProgPart.Id;
        $state.go('ProgrammePartTermAdd');
    };

    /*Update Programme Part*/
    $scope.editProgPart = function () {
        if ($scope.ProgPart.FacultyId == null || $scope.ProgPart.FacultyId === undefined
            || $scope.ProgPart.ProgrammeId == null || $scope.ProgPart.ProgrammeId === undefined
            || $scope.ProgPart.ExamPatternId == null || $scope.ProgPart.ExamPatternId === undefined
            || $scope.ProgPart.PartName == null || $scope.ProgPart.PartName === undefined
            || $scope.ProgPart.PartShortName == null || $scope.ProgPart.PartShortName === undefined
            || $scope.ProgPart.SequenceNo == null || $scope.ProgPart.SequenceNo === undefined
            || $scope.ProgPart.NoOfTerms == null || $scope.ProgPart.NoOfTerms === undefined) {

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
        else {
            $http({
                method: 'POST',
                url: 'api/MstProgrammePart/MstProgrammePartEdit',
                data: $scope.ProgPart,
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
                        alert(response.obj); 
                        $scope.showFormFlag = false;
                        $scope.getProgramPartList();

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete Programme Part*/
    $scope.deleteProgPart = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.ProgPart = data;

            $http({
                method: 'POST',
                url: 'api/MstProgrammePart/MstProgrammePartDelete',
                data: $scope.ProgPart,
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
                        alert(response.obj); 
                        $scope.getProgramPartList();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    /*Get Faculty List*/
    $scope.getFacultyList = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetForDropDown',
            //data: $scope.ProgPart,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.FacultyList = {};
                }
                else {
                    $scope.FacultyList = response.obj;
                    if ($localStorage.define.ProgrammeId) {
                        $scope.getProgrammeListByFacId();
                    }
                }
            })
            .error(function (res) {
                alert(res);
            });
    };    

    /*Get Programme List By Fac Id*/
    $scope.getProgrammeListByFacId = function () {
        
        if ($scope.ProgPart.FacultyId != null && $scope.ProgPart.FacultyId != undefined) {
            var data = { FacultyId: $scope.ProgPart.FacultyId };
        } else {
            alert("Something Went Wrong!!!")
        }

        $http({
            method: 'POST',
            url: 'api/MstProgrammePart/MstProgrammeGetByFacId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgrammeList1 = {};
                }
                else {
                    $scope.ProgrammeList1 = response.obj;
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Programme List By Faculty/Inst Id*/
    $scope.getProgrammeList = function () {
        if ($scope.ProgPart.FacultyId != null && $scope.ProgPart.FacultyId != undefined) {
            var data = { FacultyId: $scope.ProgPart.FacultyId };
        } else {
            alert("Something Went Wrong!!!")
        }

        $http({
            method: 'POST',
            url: 'api/MstProgrammePart/MstProgrammeGetByFacultyId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgrammeList = {};
                }
                else {
                    $scope.ProgrammeList = response.obj;
                }
            })
            .error(function (res) {
                alert(res);
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

    /*Modify Programme Part Data*/
    $scope.modifyProgramPartData = function (data) {
        $scope.showFormFlag = true;
        $scope.ProgPart = data;
        if (!($scope.getFacultyList == null && $scope.getFacultyList == undefined))
        { $scope.getProgrammeListByFacId(); }


    };

    /*Add New Programme Part*/
    $scope.newProgramPartAdd = function () {        
        $state.go('ProgrammePartAdd');              
    };

    /*Back to Edit Page of Programme Part*/
    $scope.backToList = function () {        
        $scope.ProgPart = {};
        $localStorage.define = {};
        $state.go('ProgrammePartEdit');        
    };

    /*Display Programme Part Data*/
    $scope.displayProgrammePart = function (data) {
        $scope.ProgPart = data;
    };

    /*Active Enable Programme Part*/
    $scope.ShowProgrammePart = function (data) {

        $scope.ProgPart = data;

        $http({
            method: 'POST',
            url: 'api/MstProgrammePart/MstProgrammePartIsActive',
            data: $scope.ProgPart,
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
                    $scope.getProgramPartList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable Programme Part*/
    $scope.HideProgrammePart = function (data) {

        $scope.ProgPart = data;

        $http({
            method: 'POST',
            url: 'api/MstProgrammePart/MstProgrammePartIsSuspended',
            data: $scope.ProgPart,
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
                    $scope.getProgramPartList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Cancel Programme Part*/
    $scope.cancelProgPart = function () {
        $localStorage.define = {};    
        $scope.ProgPart = {};

    };

    $scope.exportDataofProgrammePart = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ProgrammePartData_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Programme Part List | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'ExaminationPatternName', title: 'Examination Pattern Name' },
                { columnid: 'PartName', title: 'Programme Part Name' },
                { columnid: 'PartShortName', title: 'Programme Part Short Name' },
                { columnid: 'SequenceNo', title: 'Sequence No' },
                { columnid: 'NoOfTerms', title: 'No Of Terms' },                
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ProgrammePartData]);
    };

});