app.controller('ResGradeLevelsCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Res Grade Levels";

    $scope.cardTitle = "Res Grade Levels Operation";

    $scope.getGradeLevelList = function () {

        $http({
            method: 'POST',
            url: 'api/ResGradeLevels/ResGradeLevelsGet',
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

                        $scope.GradeLevelTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addGradeLevel = function () {
        if ($scope.GradeLevel.GradeLevelName == null || $scope.GradeLevel.GradeLevelName === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#GradeLevel')))
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
            url: 'api/ResGradeLevels/ResGradeLevelsAdd',
            data: $scope.GradeLevel,
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
                        $scope.GradeLevel = {};
                        $scope.getGradeLevelList();
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
          }
    };

    $scope.modifyGradeLevelData = function (data) {
        $scope.showFormFlag = true;
        $scope.GradeLevel = data;
        $scope.getGradeLevelList();
    };

    $scope.editGradeLevel = function () {

        if ($scope.GradeLevel.GradeLevelName == null || $scope.GradeLevel.GradeLevelName === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#GradeLevel')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Update...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
        $http({
            method: 'POST',
            url: 'api/ResGradeLevels/ResGradeLevelsEdit',
            data: $scope.GradeLevel,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //  $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.GradeLevel = {};
                        $scope.getGradeLevelList();
                        $scope.showFormFlag = false;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
          }
    };

    $scope.deleteGradeLevel = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('Record will be deleted permanently.')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');
        $mdDialog.show(confirm).then(function () {

            $scope.GradeLevel = data;
            $http({
                method: 'POST',
                url: 'api/ResGradeLevels/ResGradeLevelsDelete',
                data: $scope.GradeLevel,
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
                            $scope.GradeLevel = {};
                            $scope.getGradeLevelList();

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

    $scope.newGradeLevelAdd = function () {
        $state.go('ResGradeLevelsAdd');
    };
    $scope.nextAdd = function () {
        $state.go('ResGradeLevelsEdit');
    };
    $scope.backToList = function () {
        $state.go('ResGradeLevelsEdit');
    };
    $scope.displayGradeLevel = function (data) {
        $scope.GradeLevel = data;
    };
    $scope.cancelGradeLevel = function () {
        $scope.GradeLevel = {};
        $scope.modifyUserFlag = false;
    }

    $scope.modifyGradeLevel = function (data) {
        $scope.GradeLevel = data;
        $scope.modifyUserFlag = true;
    }

});