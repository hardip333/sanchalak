app.controller('ExamSlotMasterCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage ExamSlotMaster";

    $scope.resetExamSlotMaster = function () {
        $scope.ExamSlotMaster = {};
    }



    $scope.getExamSlotMaster = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/ExamSlotMaster/ExamSlotMasterGet',
            data: data,
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
                    $scope.ExamSlotMasterTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }
    //$scope.EvaluationGet();

    $scope.addExamSlotMaster = function () {

        if ($scope.ExamSlotMaster.SlotName === null || $scope.ExamSlotMaster.SlotName === undefined) {
            alert("Enter Slot Master Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/ExamSlotMaster/ExamSlotMasterAdd',
                data: $scope.ExamSlotMaster,
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
                        $scope.ExamSlotMaster = {};
                        $scope.getExamSlotMaster();
                        $state.go('ExamSlotMasterEdit');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    }

    //$scope.cancelUser = function () {
    //    $scope.users = {};
    //    $scope.modifyUserFlag = false;
    //}

    $scope.modifyExamSlotMaster = function (data) {
        //debugger;
        $scope.showFormFlag = true;
        //var dob = data.StartTime;
        //var ProperDate1 = dob.slice(6);
        //var ProperDate2 = ProperDate1.slice(0, ProperDate1.length - 2);
        //var ProperDate3 = parseInt(ProperDate2);
        //var item = ProperDate3;
        //var FinalTime = new Time(item);
        ////alert(FinalDate);
        //$scope.secondTime = $filter('time')(FinalTime, "HH:mm:ss.SSS");
        ////alert($scope.secondDate);

        //var dob1 = data.EndTime;
        //var ProperDate4 = dob1.slice(6);
        //var ProperDate5 = ProperDate4.slice(0, ProperDate4.length - 2);
        //var ProperDate6 = parseInt(ProperDate5);
        //var item1 = ProperDate6;
        ////var FinalTime1 = new FinalTime1(item1);
        //$scope.secondTime1 = $filter('time')(FinalTime1, "HH:mm:ss.SSS"); 
        //$scope.getSlotMaster();
        $scope.ExamSlotMaster = data;


    }

    $scope.UpdateExamSlotMaster = function () {
        if ($scope.ExamSlotMaster.SlotName === null || $scope.ExamSlotMaster.SlotName === undefined) {
            alert("Enter Exam Slot Master Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/ExamSlotMaster/ExamSlotMasterUpdate',
                data: $scope.ExamSlotMaster,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        // $scope.newUser = {};
                        $scope.getExamSlotMaster();
                        $scope.ExamSlotMaster = {};
                        // $scope.modifyExamSlotMaster = true;
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.ExamSlotMasterDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.ExamSlotMaster = data;

            $http({
                method: 'POST',
                url: 'api/ExamSlotMaster/ExamSlotMasterDelete',
                data: $scope.ExamSlotMaster,
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
                        alert("your data deleted successfully");
                        $scope.getExamSlotMaster();

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            // };
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };


    $scope.ExamExamSlotMasterCancle = function () {

        $scope.ExamSlotMaster = {};
        $scope.modifyExamSlotMaster = false;
    };
    $scope.newExamSlotMasterAdd = function () {
        $state.go('ExamSlotMasterAdd');
    };

    $scope.backToList = function () {
        $state.go('ExamSlotMasterEdit');
    };
    $scope.displayExamSlotMaster = function (data) {
        $scope.ExamSlotMaster = data;
    };

    $scope.showUser = function (data) {

        $scope.ExamSlotMaster = data;

        $http({
            method: 'POST',
            url: 'api/ExamSlotMaster/ExamSlotMasterIsActive',
            data: $scope.ExamSlotMaster,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getExamSlotMaster();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.HideUser = function (data) {

        $scope.ExamSlotMaster = data;

        $http({
            method: 'POST',
            url: 'api/ExamSlotMaster/ExamSlotMasterIsSuspended',
            data: $scope.ExamSlotMaster,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getExamSlotMaster();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});








