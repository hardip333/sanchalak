app.controller('ExamFeeConfigCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Exam Fee Configuration";
   
    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.ExamEventGet = function () {

        $http({
            method: 'POST',
            url: 'api/ExamFeeConfiguration/ExamEventMasterListGet',
            //data: SubSpecialisation,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.FacList = {};
                }
                else {

                    $scope.ExamEventList = response.obj;

                }


            })
            .error(function (res) {

            });
    };

    $scope.FacultyGetByExamEventId = function () {
        $scope.FacultyList = {};
        if ($scope.ExamFeeCon.ExamEventId == null || $scope.ExamFeeCon.ExamEventId == undefined || $scope.ExamFeeCon.ExamEventId == 0) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Exam Event Drop Down")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $http({
                method: 'POST',
                url: 'api/ExamFeeConfiguration/GetFacultyByExamEventId',
                data: $scope.ExamFeeCon,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $go.state('login');
                    }

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $scope.FacultyList = {};

                    }
                    else {
                      
                        $scope.FacultyList = response.obj;
                      
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    }
  

    $scope.ExamFeeConfigurationGetByExamEventId = function () {

        $http({
            method: 'POST',
            url: 'api/ExamFeeConfiguration/ExamFeeConfigurationGetByEventId',
            data: $scope.ExamFeeCon,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.NoRecLabel = true;
                    $scope.showTableFlag = false;
                    
                }
                else {


                    $scope.ExamFeeConfigurationTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.ExamFeeConfiguration = response.obj;
                    $scope.ExamFeeCon.ExamFeeAmount = null;
                    $scope.IsFeeAmountVisibleForAll = true;
                    $scope.IsFeeAmountVisible = false;
                    $scope.IsVisibleSelectBtn = true;
                    $scope.showTableFlag = true;
                    $scope.NoRecLabel = false;
                    $scope.Backbtn = true;
                  

                   
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
        
    }

    $scope.selectallFlag = true;
    $scope.deselectallFlag = false;

    $scope.selectall = function () {
       
        for (var i in $scope.ExamFeeConfigurationTableParams.data) {
            if ($scope.ExamFeeConfigurationTableParams.data[i].ExamFeeAmount > 0 && $scope.ExamFeeConfigurationTableParams.data[i].ExamFeePublish == 0) {
                $scope.ExamFeeConfigurationTableParams.data[i].IsCheckSelect = false;
            }
            else { $scope.ExamFeeConfigurationTableParams.data[i].IsCheckSelect = true;}
            

        }
        $scope.selectallFlag = false;
        $scope.deselectallFlag = true;
    }

    $scope.deselectall = function () {
        for (var i in $scope.ExamFeeConfigurationTableParams.data) {
            $scope.ExamFeeConfigurationTableParams.data[i].IsCheckSelect = false;

        }
        $scope.selectallFlag = true;
        $scope.deselectallFlag = false;
    }



    //Check atleast one checkbox
    $scope.AtLeastOneCheckforExamForm = function () {
        var count = 0;
        for (var i = 0; i < $scope.ExamFeeConfiguration.length; i++) {
            if ($scope.ExamFeeConfiguration[i].IsCheckSelect == null || $scope.ExamFeeConfiguration[i].IsCheckSelect === undefined
                || $scope.ExamFeeConfiguration[i].IsCheckSelect == false
            ) {
                continue;

            }
            else {
                count = count + 1;
            }
        }
        if (count > 0) {
           
            $scope.UpdateExamFeeConfiguration();

        }
        else {
           
            $scope.ExamFeeCon.ExamFeeAmount = null;
            alert("Please Select at least one checkbox");
           
        }

    }

    $scope.UpdateExamFeeConfiguration = function (ev) {
        if ($scope.ExamFeeCon.ExamFeeAmount == null || $scope.ExamFeeCon.ExamFeeAmount == undefined || $scope.ExamFeeCon.ExamFeeAmount == 0) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Enter Exam Fee Amount")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
        var confirm = $mdDialog.confirm()
            .title('Would you like to Update Exam Fee?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $scope.ExamFeeConfig = "";
        for (var i = 0; i < $scope.ExamFeeConfiguration.length; i++) {
            if ($scope.ExamFeeConfiguration[i].IsCheckSelect) {
                var CourseScheduleId = $scope.ExamFeeConfiguration[i].CourseScheduleId;
                $scope.ExamFeeConfig += + CourseScheduleId + ",";
            }
        }

        $scope.ExamFeeConfig = $scope.ExamFeeConfig.slice(0, -1);
       
        $scope.FinalModel = {};
        $scope.FinalModel.ExamFeeConfig = $scope.ExamFeeConfig;
        $scope.FinalModel.ExamFeeAmount = $scope.ExamFeeCon.ExamFeeAmount;
        $mdDialog.show(confirm).then(function () {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/ExamFeeConfiguration/UpdateExamFeeConfiguration',
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
                        
                        $scope.NoRecLabel = false;
                        $scope.Backbtn = false;
                        $scope.IsSubmitBtnVisible = false;
                        $scope.ExamFeeCon.ExamFeeAmount = null;
                        alert(response.obj);
                        $scope.offSpinner();
                        $scope.ExamFeeConfigurationGetByExamEventId();
                     
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

    $scope.ShowExamFeeAmountForm = function (data) {
        if (data.ExamFeeAmount > 0 && data.ExamFeePublish == 0) {
            for (var i = 0; i < $scope.ExamFeeConfiguration.length; i++) {
                $scope.ExamFeeConfiguration[i].IsCheckSelect = false;


            }
        }
        $scope.ExamFeeCon.ExamFeeAmount =data.ExamFeeAmount ;
        $scope.ExamFeeCon.Id=data.CourseScheduleId ;
        $scope.IsFeeAmountVisible = true;
        $scope.IsFeeAmountVisibleForAll = false;
        $scope.IsVisibleSelectBtn = false;
       
    }
    $scope.resetExamFeeConfigForParticularId = function () {
        $scope.IsFeeAmountVisible = false;
        $scope.IsFeeAmountVisibleForAll = true;
        $scope.IsVisibleSelectBtn = true;
        $scope.selectallFlag = true;
        $scope.ExamFeeCon.ExamFeeAmount = null;
    }
    $scope.EditExamFeeConfigForParticularId = function (ev) {
        
        if ($scope.ExamFeeCon.ExamFeeAmount == null || $scope.ExamFeeCon.ExamFeeAmount == undefined || $scope.ExamFeeCon.ExamFeeAmount == 0) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Enter Exam Fee Amount")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            var confirm = $mdDialog.confirm()
                .title('Would you like to Update Exam Fee?')
                .textContent('')
                .ariaLabel('Lucky day')
                .targetEvent(ev)
                .ok('Yes')
                .cancel('No');
            $mdDialog.show(confirm).then(function () {
                $scope.onSpinner();
                $http({
                    method: 'POST',
                    url: 'api/ExamFeeConfiguration/UpdateExamFeeConfigurationByCourseScheduleId',
                    data: $scope.ExamFeeCon,
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
                            //$scope.showTableFlag = false;
                            $scope.IsFeeAmountVisible = false;
                            $scope.NoRecLabel = false;
                            $scope.Backbtn = false;
                            $scope.ExamFeeCon.ExamFeeAmount = null;
                            alert(response.obj);
                            $scope.offSpinner();
                            $scope.ExamFeeConfigurationGetByExamEventId();





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
    $scope.IsVisibleSubmitBtn = function () {

        $scope.IsSubmitBtnVisible = true;
        $scope.IsHallTicketVisible = false;

    }
     
    $scope.backToList = function () {
        $scope.showTableFlag = false;
        $scope.Backbtn = false;
        $scope.ExamFeeCon = {};
        $scope.FacultyList = {};
        $scope.IsFeeAmountVisible = false;
        $scope.IsSubmitBtnVisible = false;
        $scope.IsVisibleSelectBtn = false;
    };






});