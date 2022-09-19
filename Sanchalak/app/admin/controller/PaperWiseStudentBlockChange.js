app.controller('StudentBlockChangeCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Paper Wise Student Block Change";

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }


    $scope.FlagCenterBranch = false;
    $rootScope.showLoading = false;
    $scope.FlagBlock = false;
    $scope.EventdropdownFlag = true;
    $scope.showButtonFlag = true;

    $scope.selectallFlag = true;
    $scope.deselectallFlag = false;

    $scope.selectall = function () {

        for (var i in $scope.StudentDetailsPaperWiseParams.data) {
          
            $scope.StudentDetailsPaperWiseParams.data[i].IsCheckSelect = true; 
           

        }
       
        $scope.selectallFlag = false;
        $scope.deselectallFlag = true;
        $scope.IsBlockDropDownForAll = true;
    }

    $scope.deselectall = function () {
        for (var i in $scope.StudentDetailsPaperWiseParams.data) {
            $scope.StudentDetailsPaperWiseParams.data[i].IsCheckSelect = false;


        }
        $scope.selectallFlag = true;
        $scope.deselectallFlag = false;
        $scope.ChangeExamBlockList = {};
        $scope.ExamVenueExamCenterList = {};
    }

    //Check atleast one checkbox
    $scope.AtLeastOneCheckforBlockChange = function () {
        var count = 0;
        for (var i = 0; i < $scope.StudentDetailsPaperWise.length; i++) {
            if ($scope.StudentDetailsPaperWise[i].IsCheckSelect == null || $scope.StudentDetailsPaperWise[i].IsCheckSelect === undefined
                || $scope.StudentDetailsPaperWise[i].IsCheckSelect == false
            ) {
                continue;

            }
            else {
                count = count + 1;
            }
        }
        if (count > 0) {

            $scope.UpdateStudentBlockChange();

        }
        else {

           
            alert("Please Select at least one checkbox");

        }

    }

   
    
    $scope.backButtonStudentFlag = function () {
        $scope.EventdropdownFlag = true;
        $scope.FlagCenterBranch = true;
        $scope.FlagBlock = true;
        $scope.showExamDateDetailsFlag = true;
        $scope.showStudentDetailsFlag = false;
            
    }

   
    
    $scope.pendingMstSpecialisationGetByPId = function () {
       
        $rootScope.showLoading = true;
        $scope.FlagCenterBranch = true;

        $http({
            method: 'POST',        
            url: 'api/StudentBlockChange/MstSpecialisationGetByPId',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {

                    $scope.MstSpecialisationGetByPIdList = response.obj.SpecialisationsList;
                    $scope.details = response.obj;

                }


            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    $scope.getVenueList = function () {

        $http({
            method: 'POST',
            url: 'api/StudentBlockChange/ExamVenueGetByPIPTId',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {

                    
                    $scope.ExamVenueList = response.obj;
                    //$scope.isPaperWiseAllocationChecked = true;
                  
                  
                }

            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
        //}


    }
    
    $scope.getBlockList = function () {
        if ($scope.filter.ExamVenueExamCenterId === null || $scope.filter.ExamVenueExamCenterId === undefined || $scope.filter.ExamVenueExamCenterId === ""
           


        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select the Exam Center")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.filter.BranchId === null || $scope.filter.BranchId === undefined || $scope.filter.BranchId === "")
        {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select the Branch")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $http({
                method: 'POST',
                url: 'api/StudentBlockChange/ExamBlockGetByVenueIdPIPTId',
                data: $scope.filter,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        alert(response.obj);
                    }
                    else {

                        
                        $scope.ExamBlockList = response.obj;
                        $scope.FlagBlock = true;
                      
                    }

                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });
        }


    }

    $scope.getBlockListforChange = function () {   
            $http({
                method: 'POST',
                url: 'api/StudentBlockChange/NotSelectedExamBlockGetByVenueIdPIPTId',
                data: $scope.filter,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        alert(response.obj);
                    }
                    else {

                     
                        $scope.ChangeExamBlockList = response.obj;
                       

                    }

                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });
       


    }


    

    $scope.showExamDetailsForParticularBlock = function () {
        if ($scope.filter.ExamBlockId === null || $scope.filter.ExamBlockId === undefined || $scope.filter.ExamBlockId === ""



        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select the Block")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/StudentBlockChange/ExamDateGetByExamBlockIdForStudentBlockChange',
                data: $scope.filter,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        alert(response.obj);
                        $scope.offSpinner();

                    }
                    else {
                        $scope.ExamDateDetailsParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        })

                        $scope.offSpinner();

                        $scope.showExamDateDetailsFlag = true;


                    }


                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });

        }
    };

    $scope.GetStudentForPaperWiseBlockChange = function (data) {

        $scope.filter.MstPaperId = data.MstPaperId;
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/StudentBlockChange/GetStudentForPaperWiseBlockChange',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.offSpinner();

                    $scope.StudentDetailsPaperWise = response.obj;
                    for (let i = 0; i < $scope.StudentDetailsPaperWise.length; i++) {

                        $scope.StudentDetails = {};
                        
                        $scope.StudentDetails.InstancePartTermName = $scope.StudentDetailsPaperWise[i].InstancePartTermName;
                        $scope.StudentDetails.BranchName = $scope.StudentDetailsPaperWise[i].BranchName;
                        $scope.StudentDetails.ProgrammeName = $scope.StudentDetailsPaperWise[i].ProgrammeName;
                        $scope.StudentDetails.VenueName = $scope.StudentDetailsPaperWise[i].VenueName;
                        $scope.StudentDetails.CenterName = $scope.StudentDetailsPaperWise[i].CenterName;
                        $scope.StudentDetails.ExamBlockName = $scope.StudentDetailsPaperWise[i].ExamBlockName;
                        $scope.StudentDetails.SlotName = $scope.StudentDetailsPaperWise[i].SlotName;
                        $scope.StudentDetails.ExamEventName = $scope.StudentDetailsPaperWise[i].ExamEventName;
                        $scope.StudentDetails.ExamDateView = $scope.StudentDetailsPaperWise[i].ExamDateView;
                        $scope.StudentDetails.PaperCode = $scope.StudentDetailsPaperWise[i].PaperCode;
                        $scope.StudentDetails.PaperName = $scope.StudentDetailsPaperWise[i].PaperName;
                     


                    }
                    $scope.StudentDetailsPaperWiseParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    })
                    $scope.showStudentDetailsFlag = true;
                    $scope.showExamDateDetailsFlag = false;                                 
                    $scope.FlagCenterBranch = false;               
                    $scope.FlagBlock = false;
                    $scope.EventdropdownFlag = false;

                    $scope.getBlockListforChange();
                    $scope.ExamVenueExamCenterGetforStudentBlockChange();


                }


            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });


    };

  
    $scope.ExamCenterGetforStudentBlockChange = function () {
        
        $scope.FlagCenterBranch = true;

        $http({
            method: 'POST',
            url: 'api/StudentBlockChange/ExamCenterGetForStudentBlockChange',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamCenterTableParams = new NgTableParams({}, { dataset: response.obj });
                $scope.ExamCenterList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });

    };

    $scope.ExamVenueExamCenterGetforStudentBlockChange = function () {


        $http({
            method: 'POST',
            url: 'api/StudentBlockChange/ExamVenueExamCenterGetForStudentBlockChange',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
              
                $scope.ExamVenueExamCenterList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });

    };

    $scope.UpdateStudentBlockChange = function (ev) {
        if ($scope.filter.ExamVenExamCentId == null || $scope.filter.ExamVenExamCentId == undefined || $scope.filter.ExamVenExamCentId == "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Exam Center")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.filter.ExamBlkId == null || $scope.filter.ExamBlkId == undefined || $scope.filter.ExamBlkId == "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Exam Block")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        
        else {
            var confirm = $mdDialog.confirm()
                .title('Would you like to Update Student Block?')
                .textContent('')
                .ariaLabel('Lucky day')
                .targetEvent(ev)
                .ok('Yes')
                .cancel('No');
            
            $scope.ExamStudentBlockChange = "";
            var StudentPRNCount = 0;
            $scope.TotalCountofIncStudentExamPaperMapId = "";
            for (var i = 0; i < $scope.StudentDetailsPaperWise.length; i++) {
              
                //if ($scope.StudentDetailsPaperWise[i].IsCheckSelect) {
                //    //var IncStudentExamPaperMapId = $scope.StudentDetailsPaperWise[i].IncStudentExamPaperMapId;
                //    //$scope.ExamStudentBlockChange += + IncStudentExamPaperMapId + ",";
                   
                    if ($scope.StudentDetailsPaperWise[i].IndexId> 0 && $scope.StudentDetailsPaperWise[i].IsCheckSelect==true) {
                        StudentPRNCount++;
                    }
               // }
             
            }
            
           // alert(StudentPRNCount);
           // $scope.ExamStudentBlockChange = $scope.ExamStudentBlockChange.slice(0, -1);
         
           
                   
            for (var i in $scope.ChangeExamBlockList) {
                //debugger
                if ($scope.ChangeExamBlockList[i].ExamBlkId == $scope.filter.ExamBlkId) {
                    $scope.filter.AvailableCapacity = $scope.ChangeExamBlockList[i].AvailableCapacity;
                    if ($scope.ChangeExamBlockList[i].AvailableCapacity < StudentPRNCount) {

                        alert('Your Available Capacity is less');

                    }

                }
            }
            $scope.FinalModel = {};
            debugger
            $scope.FinalModel.StudentList=$scope.StudentDetailsPaperWise;
            //$scope.FinalModel.ExamStudentBlockChange = $scope.ExamStudentBlockChange;
            $scope.FinalModel.MstPaperId = $scope.filter.MstPaperId;
            $scope.FinalModel.ExamMasterId = $scope.filter.ExamMasterId;
          
            $scope.FinalModel.ExamVenueId = $scope.filter.ExamVenueId;
            $scope.FinalModel.BranchId = $scope.filter.BranchId;
            $scope.FinalModel.ProgrammePartTermId = $scope.filter.ProgrammePartTermId;
            $scope.FinalModel.ExamBlockIdNew = $scope.filter.ExamBlkId;
            $scope.FinalModel.ExamBlockIdOld = $scope.filter.ExamBlockId;
            $scope.FinalModel.ExamVenueExamCenterId = $scope.filter.ExamVenExamCentId;
            console.log($scope.FinalModel);
            $mdDialog.show(confirm).then(function () {
                $scope.onSpinner();
                $http({
                    method: 'POST',
                    url: 'api/StudentBlockChange/UpdateStudentBlockChange',
                    data: $scope.FinalModel,
                    eaders: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            //$rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                            alert(response.obj);
                           $scope.offSpinner();


                        }
                        else {

                            
                            alert(response.obj);
                            $scope.offSpinner();
                            //$scope.ExamFeeConfigurationGetByExamEventId();

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });

            }, function () {
                $scope.status = 'You decided to keep your debt.';
            });
        }
    }


   

   

});