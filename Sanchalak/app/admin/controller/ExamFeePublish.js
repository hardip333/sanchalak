app.controller('ExamFeePublishCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Exam Fee Publish";

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
            url: 'api/ExamFeePublishConfiguration/ExamEventMasterListGet',
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
        if ($scope.ExamFeePublish.ExamEventId == null || $scope.ExamFeePublish.ExamEventId == undefined || $scope.ExamFeePublish.ExamEventId == 0) {
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
                url: 'api/ExamFeePublishConfiguration/GetFacultyByExamEventId',
                data: $scope.ExamFeePublish,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $go.state('login');
                    }

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);


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

    $scope.ExamFeePublishConfigurationGetByExamEventId = function () {
        debugger
      
            $http({
                method: 'POST',
                url: 'api/ExamFeePublishConfiguration/ExamFeePublishConfigurationGetByEventId',
                data: $scope.ExamFeePublish,
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


                        $scope.ExamFeePublishConfigurationTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                        $scope.ExamFeePublishConfiguration = response.obj;
                        $scope.showTableFlag = true;
                        $scope.NoRecLabel = false;
                        $scope.Backbtn = true;



                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
       
    }
    $scope.UpdateExamFeePublishConfig = function (data,ev) {
      
        $scope.ExamFeePublish.Id = data.CourseScheduleId;
            var confirm = $mdDialog.confirm()
                .title('Would you like to Publish Exam Fee?')
                .textContent('')
                .ariaLabel('Lucky day')
                .targetEvent(ev)
                .ok('Yes')
                .cancel('No');
            $mdDialog.show(confirm).then(function () {
                $scope.onSpinner();
                $http({
                    method: 'POST',
                    url: 'api/ExamFeePublishConfiguration/UpdateExamFeePublishConfiguration',
                    data: $scope.ExamFeePublish,
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
                            $scope.ExamFeePublishConfigurationGetByExamEventId();
                            alert(response.obj);
                            $scope.offSpinner();

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
    $scope.UpdateExamFeeUnPublishConfig = function (data,ev) {
       
        $scope.ExamFeePublish.Id = data.CourseScheduleId;
        var confirm = $mdDialog.confirm()
            .title('Would you like to UnPublish Exam Fee?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');
        $mdDialog.show(confirm).then(function () {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/ExamFeePublishConfiguration/UpdateExamFeeUnPublishConfiguration',
                data: $scope.ExamFeePublish,
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
                        $scope.NoRecLabel = false;
                        //$scope.Backbtn = false;
                        //$scope.ExamFeePublish = {};
                        $scope.ExamFeePublishConfigurationGetByExamEventId();
                        alert(response.obj);
                        $scope.offSpinner();

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
    $scope.IsVisibleSubmitBtn = function () {
       $scope.IsSubmitBtnVisible = true;
    }
    $scope.backToList = function () {
        $scope.showTableFlag = false;
        $scope.IsSubmitBtnVisible = false;
        $scope.Backbtn = false;
        $scope.ExamFeePublish = {};
    };






});