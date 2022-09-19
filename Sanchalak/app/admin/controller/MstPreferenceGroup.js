app.controller('MstPreferenceGroupCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage MstPreferenceGroup";

    $scope.resetMstPreferenceGroup = function () {
        $scope.MstPreferenceGroup = {};
    }

    $scope.MstPreferenceGroupGet = function () {
        
        var data = new Object();
        
        $http({
            method: 'POST',
            url: 'api/MstPreferenceGroup/MstPreferenceGroupGet',
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
                    $scope.MstPreferenceGroupTableParams = new NgTableParams({
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

    $scope.MstPreferenceGroupAdd = function () {
        
        if ($scope.MstPreferenceGroup.GroupName === null || $scope.MstPreferenceGroup.GroupName === undefined)
        {
            alert("Enter Group Name");
        }
        else {
            
            $http({
                method: 'POST',
                url: 'api/MstPreferenceGroup/MstPreferenceGroupAdd',
                data: $scope.MstPreferenceGroup,
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
                        $scope.MstPreferenceGroup = {};
                        $scope.MstPreferenceGroupGet();
                        $state.go('MstPreferenceGroupEdit');
                        
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

    $scope.modifyMstPreferenceGroup = function (data) {
        
        $scope.showFormFlag = true;
        $scope.MstPreferenceGroup = data;
    }

    $scope.MstPreferenceGroupEdit = function () {
        if ($scope.MstPreferenceGroup.GroupName === null || $scope.MstPreferenceGroup.GroupName === undefined) {
            alert("Enter Group Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstPreferenceGroup/MstPreferenceGroupUpdate',
                data: $scope.MstPreferenceGroup,
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
                        $scope.MstPreferenceGroupGet();
                        $scope.modifyUserFlag = false;
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.MstPreferenceGroupDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstPreferenceGroup = data;

            $http({
                method: 'POST',
                url: 'api/MstPreferenceGroup/MstPreferenceGroupDelete',
                data: $scope.MstPreferenceGroup,
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
                        $scope.MstPreferenceGroupGet();
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
   
    $scope.MstPreferenceGroupCancle = function () {

        $scope.MstPreferenceGroup = {};
        $scope.modifyMstPreferenceGroupFlag = false;
    };
    $scope.newMstPreferenceGroupAdd = function () {
        $state.go('MstPreferenceGroupAdd');
    };

    $scope.backToList = function () {
        $state.go('MstPreferenceGroupEdit');
    };
    $scope.displayMstPreferenceGroup = function (data) {
        $scope.MstPreferenceGroup = data;
    };

    $scope.showUser = function (data) {

        $scope.MstPreferenceGroup = data;

        $http({
            method: 'POST',
            url: 'api/MstPreferenceGroup/MstPreferenceGroupIsActive',
            data: $scope.MstPreferenceGroup,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.MstPreferenceGroupGet();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.HideUser = function (data) {

        $scope.MstPreferenceGroup = data;

        $http({
            method: 'POST',
            url: 'api/MstPreferenceGroup/MstPreferenceGroupIsSuspended',
            data: $scope.MstPreferenceGroup,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.MstPreferenceGroupGet();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});






    

