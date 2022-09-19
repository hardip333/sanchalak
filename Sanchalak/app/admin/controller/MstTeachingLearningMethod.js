app.controller('MstTeachingLearningMethodCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage MstTeachingLearningMethod";

    /*Reset TeachingLearning Method*/
    $scope.resetTeachingLearningMethod = function () {
        $scope.MstTeachingLearningMethod = {};
    };

    /*Get TeachingLearning Method List*/
    $scope.getTeachingLearningMethod = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstTeachingLearningMethod/MstTeachingLearningMethodGet',
            
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
                    $scope.MstTeachingLearningMethodTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };    

    /*Add TeachingLearning Method*/
    $scope.addTeachingLearningMethod = function () {
        if ($scope.MstTeachingLearningMethod.TeachingLearningMethodName === null || $scope.MstTeachingLearningMethod.TeachingLearningMethodName === undefined) { alert("Enter All Fields"); }
        else {

            $http({
                method: 'POST',
                url: 'api/MstTeachingLearningMethod/MstTeachingLearningMethodAdd',
                data: $scope.MstTeachingLearningMethod,
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
                        $scope.MstTeachingLearningMethod = {};
                        $scope.getTeachingLearningMethod();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify TeachingLearning Method Data*/
    $scope.modifyTeachingLearningMethodData = function (data) {
        $scope.MstTeachingLearningMethod = data;
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update TeachingLearning Method*/
    $scope.editTeachingLearningMethod = function () {
        if ($scope.MstTeachingLearningMethod.TeachingLearningMethodName === null || $scope.MstTeachingLearningMethod.TeachingLearningMethodName === undefined) { alert("Enter All Fields"); }
        else {

            $http({
                method: 'POST',
                url: 'api/MstTeachingLearningMethod/MstTeachingLearningMethodEdit',
                data: $scope.MstTeachingLearningMethod,
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
                        $scope.getTeachingLearningMethod();                        
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete TeachingLearning Method*/
    $scope.deleteTeachingLearningMethod = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstTeachingLearningMethod = data;

            $http({
                method: 'POST',
                url: 'api/MstTeachingLearningMethod/MstTeachingLearningMethodDelete',
                data: $scope.MstTeachingLearningMethod,
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
                        $scope.getTeachingLearningMethod();
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

    /*Add New TeachingLearning Method*/
    $scope.newTeachingLearningMethodAdd = function () {
        $state.go('teachingLearningMethodAdd');
    };

    /*Back to Edit Page of TeachingLearning Method*/
    $scope.backToList = function () {
        $state.go('teachingLearningMethodEdit');
    };

    /*Active Enable TeachingLearning Method*/
    $scope.ShowTeachingLearningMethod = function (data) {

        $scope.MstTeachingLearningMethod = data;

        $http({
            method: 'POST',
            url: 'api/MstTeachingLearningMethod/MstTeachingLearningMethodIsActive',
            data: $scope.MstTeachingLearningMethod,
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
                    $scope.getTeachingLearningMethod();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable TeachingLearning Method*/
    $scope.HideTeachingLearningMethod = function (data) {

        $scope.MstTeachingLearningMethod = data;

        $http({
            method: 'POST',
            url: 'api/MstTeachingLearningMethod/MstTeachingLearningMethodIsSuspended',
            data: $scope.MstTeachingLearningMethod,
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
                    $scope.getTeachingLearningMethod();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
           });
        };
    
});






    

