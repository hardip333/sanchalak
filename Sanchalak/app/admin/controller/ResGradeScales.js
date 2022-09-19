app.controller('ResGradeScalesCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Res Grade Scales";

    $scope.cardTitle = "Res Grade Scales Operation";

    $scope.getGradeScaleList = function () {

        $http({
            method: 'POST',
            url: 'api/ResGradeScales/ResGradeScalesGet',
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

                        $scope.GradeScaleTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addGradeScale = function () {
        if ($scope.GradeScale.GradeScaleName == null || $scope.GradeScale.GradeScaleName === undefined ||
            $scope.GradeScale.MaxGradePoints == null || $scope.GradeScale.MaxGradePoints === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#GradeScale')))
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
            url: 'api/ResGradeScales/ResGradeScalesAdd',
            data: $scope.GradeScale,
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
                        $scope.GradeScale = {};
                        $scope.getGradeScaleList();
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
          }
    };

    $scope.modifyGradeScaleData = function (data) {
        $scope.showFormFlag = true;
        $scope.GradeScale = data;
        $scope.getGradeScaleList();
    };

    $scope.editGradeScale = function () {

        if ($scope.GradeScale.GradeScaleName == null || $scope.GradeScale.GradeScaleName === undefined ||
            $scope.GradeScale.MaxGradePoints == null || $scope.GradeScale.MaxGradePoints === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
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
            url: 'api/ResGradeScales/ResGradeScalesEdit',
            data: $scope.GradeScale,
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
                        $scope.GradeScale = {};
                        $scope.getGradeScaleList();
                        $scope.showFormFlag = false;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
          }
    };

    $scope.deleteGradeScale = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('Record will be deleted permanently.')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');
        $mdDialog.show(confirm).then(function () {

            $scope.GradeScale = data;
            $http({
                method: 'POST',
                url: 'api/ResGradeScales/ResGradeScalesDelete',
                data: $scope.GradeScale,
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
                            $scope.GradeScale = {};
                            $scope.getGradeScaleList();

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

    $scope.newGradeScaleAdd = function () {
        $state.go('ResGradeScalesAdd');
    };
    $scope.nextAdd = function () {
        $state.go('ResGradeScalesEdit');
    };
    $scope.backToList = function () {
        $state.go('ResGradeScalesEdit');
    };
    $scope.displayGradeScale = function (data) {
        $scope.GradeScale = data;
    };
    $scope.cancelGradeScale = function () {
        $scope.GradeScale = {};
        $scope.modifyUserFlag = false;
    }

    $scope.modifyGradeScale = function (data) {
        $scope.GradeScale = data;
        $scope.modifyUserFlag = true;
    }

});