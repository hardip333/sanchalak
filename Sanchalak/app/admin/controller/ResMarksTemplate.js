app.controller('ResMarksTemplateCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Marks Template";

    $scope.cardTitle = "Marks Template Operation";
   
    $scope.getMarksTemplateList = function () {

        $http({
            method: 'POST',
            url: 'api/ResMarksTemplate/ResMarksTemplateGet',
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

                        $scope.MarksTempTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addMarksTemplate = function () {
        if ($scope.Template.NoofIntervals == null || $scope.Template.NoofIntervals === undefined ||
            $scope.Template.MarksTemplateName == null || $scope.Template.MarksTemplateName === undefined ||
            $scope.Template.MarksTemplateDescription == null || $scope.Template.MarksTemplateDescription === undefined ||
            $scope.Template.TemplateUsedwith == null || $scope.Template.TemplateUsedwith === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
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
            url: 'api/ResMarksTemplate/ResMarksTemplateAdd',
            data: $scope.Template,
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
                        $scope.Template = {};
                        $scope.getMarksTemplateList();
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
         }
    };

    $scope.modifyMarksTemplateData = function (data) {
        $scope.showFormFlag = true;
        $scope.Template = data;
        $scope.getMarksTemplateList();
    };

    $scope.editMarksTemplate = function () {

        if ($scope.Template.NoofIntervals == null || $scope.Template.NoofIntervals === undefined ||
            $scope.Template.MarksTemplateName == null || $scope.Template.MarksTemplateName === undefined ||
            $scope.Template.MarksTemplateDescription == null || $scope.Template.MarksTemplateDescription === undefined ||
            $scope.Template.TemplateUsedwith == null || $scope.Template.TemplateUsedwith === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Edit...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
        $http({
            method: 'POST',
            url: 'api/ResMarksTemplate/ResMarksTemplateEdit',
            data: $scope.Template,
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
                        $scope.Marks = {};
                        $scope.getMarksTemplateList();
                        $scope.showFormFlag = false;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
         }
    };

    $scope.deleteMarksTemplate = function (ev, data) {
       
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('Record will be deleted permanently.')             
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');
        $mdDialog.show(confirm).then(function () {

            $scope.Template = data;
            $http({
                method: 'POST',
                url: 'api/ResMarksTemplate/ResMarksTemplateDelete',
                data: $scope.Template,
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
                            $scope.Template = {};
                            $scope.getMarksTemplateList();

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

    $scope.newMarksTemplateAdd = function () {
        $state.go('ResMarksTemplateAdd');
    };
    $scope.nextAdd = function () {
        $state.go('ResMarksTemplateEdit');
    };
    $scope.backToList = function () {
        $state.go('ResMarksTemplateEdit');
    };
    $scope.displayMarksTemplate = function (data) {
        $scope.Template = data;
    };
    $scope.cancelMarksTemplate = function () {
        $scope.Template = {};
        $scope.modifyUserFlag = false;
    }

    $scope.modifyResMarksLevel = function (data) {
        $scope.Template = data;
        $scope.modifyUserFlag = true;
    }

});