app.controller('AssessmentMethodCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage MstAssessmentMethod";

    /*Reset AssessmentMethod*/
    $scope.resetAssessmentMethod = function () {
        $scope.MstAssessmentMethod = {};
    };

    /*Get AssessmentMethod*/
    $scope.getAssessmentMethod = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstAssessmentMethod/MstAssessmentMethodGet',
            
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
                    $scope.MstAssessmentMethodTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add AssessmentMethod*/
    $scope.addAssessmentMethod = function () {
        if ($scope.MstAssessmentMethod.AssessmentMethodName === null || $scope.MstAssessmentMethod.AssessmentMethodName === undefined) { alert("Enter All Fields"); }
        else {

            $http({
                method: 'POST',
                url: 'api/MstAssessmentMethod/MstAssessmentMethodAdd',
                data: $scope.MstAssessmentMethod,
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
                        $scope.MstAssessmentMethod = {};
                        $scope.getAssessmentMethod()                        

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };   

    /*Modify AssessmentMethod Data*/
    $scope.modifyAssessmentMethodData = function (data) {
        $scope.MstAssessmentMethod = data;
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update AssessmentMethod*/
    $scope.editAssessmentMethod = function () {
        if ($scope.MstAssessmentMethod.AssessmentMethodName === null || $scope.MstAssessmentMethod.AssessmentMethodName === undefined) { alert("Enter All Fields"); }
        else {
            $http({
                method: 'POST',
                url: 'api/MstAssessmentMethod/MstAssessmentMethodUpdate',
                data: $scope.MstAssessmentMethod,
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
                        $scope.getAssessmentMethod();
                        $scope.modifyUserFlag = false;
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete AssessmentMethod*/
    $scope.deleteAssessmentMethod = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstAssessmentMethod = data;

            $http({
                method: 'POST',
                url: 'api/MstAssessmentMethod/MstAssessmentMethodDelete',
                data: $scope.MstAssessmentMethod,
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
                        $scope.getAssessmentMethod();
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

    /*Add New AssessmentMethod*/
    $scope.newAssessmentMethodAdd = function () {
        $state.go('assessmentMethodAdd');
    };

    /*Back to Edit Page of AssessmentMethod*/
    $scope.backToList = function () {
        $state.go('assessmentMethodEdit');
    };

    /*Display AssessmentMethod Data*/
    $scope.displayAssessmentMethod = function (data) {
        $scope.MstAssessmentMethod = data;
    };

    /*Active Enable AssessmentMethod*/
    $scope.ShowAssessmentMethod = function (data) {

        $scope.MstAssessmentMethod = data;

        $http({
            method: 'POST',
            url: 'api/MstAssessmentMethod/MstAssessmentMethodIsActive',
            data: $scope.MstAssessmentMethod,
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
                    $scope.getAssessmentMethod();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable AssessmentMethod*/
    $scope.HideAssessmentMethod = function (data) {

        $scope.MstAssessmentMethod = data;

        $http({
            method: 'POST',
            url: 'api/MstAssessmentMethod/MstAssessmentMethodIsSuspended',
            data: $scope.MstAssessmentMethod,
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
                    $scope.getAssessmentMethod();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
           });
        };
    
});






    

