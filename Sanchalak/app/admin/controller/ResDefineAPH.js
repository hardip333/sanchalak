app.controller('ResDefineAPHCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Res Define APH";

    $scope.cardTitle = "Res Define APH Operation";

    $scope.ResAPH = {};

    $scope.getResAPHMstEvaluation = function () {

        $http({
            method: 'POST',
            url: 'api/ResCalculation/ResMstEvaluationGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.EvaluationList = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getResAPHList = function () {

        $http({
            method: 'POST',
            url: 'api/ResDefineAPH/ResDefineAPHGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {

                        $scope.ResAPHTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addResAPH = function () {
        if ($scope.ResAPH.APHName == null || $scope.ResAPH.APHName === undefined ||
            $scope.ResAPH.APHAbbrev == null || $scope.ResAPH.APHAbbrev === undefined ||
            $scope.ResAPH.APHCode == null || $scope.ResAPH.APHCode === undefined ||
            $scope.ResAPH.APHDescription == null || $scope.ResAPH.APHDescription === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#ResDefineAPH')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Add...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $http({
                method: 'POST',
                url: 'api/ResDefineAPH/ResDefineAPHAdd',
                data: $scope.ResAPH,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            alert(response.obj);
                            $scope.ResAPH = {};
                            $scope.getResAPHList();
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

$scope.modifyResAPHData = function (data) {
        $scope.showFormFlag = true;
    $scope.ResAPH = data;
    $scope.getResAPHList();
    };

    $scope.editResAPH = function () {

        if ($scope.ResAPH.APHName == null || $scope.ResAPH.APHName === undefined ||
            $scope.ResAPH.APHAbbrev == null || $scope.ResAPH.APHAbbrev === undefined ||
            $scope.ResAPH.APHCode == null || $scope.ResAPH.APHCode === undefined ||
            $scope.ResAPH.APHDescription == null || $scope.ResAPH.APHDescription === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#ResDefineAPH')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Add...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $http({
                method: 'POST',
                url: 'api/ResDefineAPH/ResDefineAPHEdit',
                data: $scope.ResAPH,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                   
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            alert(response.obj);
                            $scope.ResAPH = {};
                            $scope.getResAPHList();
                            $scope.showFormFlag = false;
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

$scope.deleteResAPH = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('Record will be deleted permanently.')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');
        $mdDialog.show(confirm).then(function () {

            $scope.ResAPH = data;
            $http({
                method: 'POST',
                url: 'api/ResDefineAPH/ResDefineAPHDelete',
                data: $scope.ResAPH,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            alert(response.obj);
                            $scope.ResAPH = {};
                            $scope.getResAPHList();

                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        },
            function () {
                $scope.status = 'You decided to keep your debt.';
            });
    };

$scope.newResAPHAdd = function () {
    $state.go('AdditionalPassingHead');
    };
    $scope.nextAdd = function () {
        $state.go('AdditionalPassingHeadEdit');
    };
    $scope.backToList = function () {
        $state.go('AdditionalPassingHeadEdit');
    };
$scope.displayResAPH = function (data) {
    $scope.ResAPH = data;
    };
$scope.cancelResAPH = function () {
    $scope.ResAPH = {};
        $scope.modifyUserFlag = false;
    }

    $scope.modifyResAPH = function (data) {
        $scope.ResAPH = data;
        $scope.modifyUserFlag = true;
    };
    $scope.MoveAPHConfig = function (Id) {
        $localStorage.APHId = Id;
        $state.go('AdditionalPassingHeadConfigAdd');
    };

});