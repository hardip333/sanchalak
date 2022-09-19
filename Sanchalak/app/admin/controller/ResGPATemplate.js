app.controller('ResGPATemplateCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Res GPA Template";

    $scope.cardTitle = "Res GPA Template Operation";

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
                        $scope.GradeScaleList = response.obj
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getMstEvaluationList = function () {

        $http({
            method: 'POST',
            url: 'api/ResGPATemplate/ResMstEvaluationGet',
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
                        $scope.MstEvaluationList = response.obj
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getGPATemplateList = function () {

        $http({
            method: 'POST',
            url: 'api/ResGPATemplate/ResGPATemplateGet',
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

                        $scope.GPATemplateTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addGPATemplate = function () {
        if ($scope.GTemplate.GradeScaleId == null || $scope.GTemplate.GradeScaleId === undefined ||
            $scope.GTemplate.MstEvaluationId == null || $scope.GTemplate.MstEvaluationId === undefined ||
            $scope.GTemplate.GPATemplateName == null || $scope.GTemplate.GPATemplateName === undefined ||
            $scope.GTemplate.GPATemplateDescription == null || $scope.GTemplate.GPATemplateDescription === undefined ||
            $scope.GTemplate.NoofIntervals == null || $scope.GTemplate.NoofIntervals === undefined

        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#GPATemplate')))
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
            url: 'api/ResGPATemplate/ResGPATemplateAdd',
            data: $scope.GTemplate,
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
                        $scope.GTemplate = {};
                        $scope.getGPATemplateList();
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
          }
    };

    $scope.modifyGPATemplateData = function (data) {
        $scope.showFormFlag = true;
        $scope.GTemplate = data;
        $scope.getGPATemplateList();
    };

    $scope.editGPATemplate = function () {

        if ($scope.GTemplate.GradeScaleId == null || $scope.GTemplate.GradeScaleId === undefined ||
            $scope.GTemplate.MstEvaluationId == null || $scope.GTemplate.MstEvaluationId === undefined ||
            $scope.GTemplate.GPATemplateName == null || $scope.GTemplate.GPATemplateName === undefined ||
            $scope.GTemplate.GPATemplateDescription == null || $scope.GTemplate.GPATemplateDescription === undefined ||
            $scope.GTemplate.NoofIntervals == null || $scope.GTemplate.NoofIntervals === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#GPATemplate')))
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
            url: 'api/ResGPATemplate/ResGPATemplateEdit',
            data: $scope.GTemplate,
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
                        $scope.GTemplate = {};
                        $scope.getGPATemplateList();
                        $scope.showFormFlag = false;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
         }
    };

    $scope.deleteGPATemplate = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('Record will be deleted permanently.')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');
        $mdDialog.show(confirm).then(function () {

            $scope.GTemplate = data;
            $http({
                method: 'POST',
                url: 'api/ResGPATemplate/ResGPATemplateDelete',
                data: $scope.GTemplate,
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
                            $scope.GTemplate = {};
                            $scope.getGPATemplateList();

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

    $scope.newGPATemplateAdd = function () {
        $state.go('ResGPATemplateAdd');
    };
    $scope.nextAdd = function () {
        $state.go('ResGPATemplateEdit');
    };
    $scope.backToList = function () {
        $state.go('ResGPATemplateEdit');
    };
    $scope.displayGPATemplate = function (data) {
        $scope.GTemplate = data;
    };
    $scope.cancelGPATemplate = function () {
        $scope.GTemplate = {};
        $scope.modifyUserFlag = false;
    }

    $scope.modifyGPATemplate = function (data) {
        $scope.GTemplate = data;
        $scope.modifyUserFlag = true;
    }

});