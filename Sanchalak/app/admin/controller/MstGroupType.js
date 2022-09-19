app.controller('MstGroupTypeCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage MstGroupType";

    $scope.resetMstGroupType = function () {
        $scope.MstGroupType = {};
    }

    $scope.MstGroupTypeGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstGroupType/MstGroupTypeGet',
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
                    $scope.MstGroupTypeTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }
    //$scope.MstGroupType();

    $scope.MstGroupTypeAdd = function () {

        if ($scope.MstGroupType.GroupTypeName === null || $scope.MstGroupType.GroupTypeName === undefined) {
            alert("Enter Group Type");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstGroupType/MstGroupTypeAdd',
                data: $scope.MstGroupType,
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
                        $scope.MstGroupType = {};
                        $scope.MstGroupTypeGet();
                        $state.go('MstGroupTypeEdit');

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

    $scope.modifyMstGroupType = function (data) {

        $scope.showFormFlag = true;
        $scope.MstGroupType = data;
    }

    $scope.MstGroupTypeEdit = function () {
        if ($scope.MstGroupType.GroupTypeName === null || $scope.MstGroupType.GroupTypeName === undefined) {
            alert("Enter Group Code");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstGroupType/MstGroupTypeUpdate',
                data: $scope.MstGroupType,
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
                        // $scope.newUser = {};
                        $scope.MstGroupTypeGet();
                        $scope.modifyUserFlag = false;
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.MstGroupTypeDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstGroupType = data;

            $http({
                method: 'POST',
                url: 'api/MstGroupType/MstGroupTypeDelete',
                data: $scope.MstGroupType,
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
                        $scope.MstGroupTypeGet();
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

    $scope.MstGroupTypeCancle = function () {

        $scope.MstGroupType = {};
        $scope.modifyMstGroupTypeFlag = false;
    };
    $scope.newMstGroupTypeAdd = function () {
        $state.go('MstGroupTypeAdd');
    };

    $scope.backToList = function () {
        $state.go('MstGroupTypeEdit');
    };
    $scope.displayMstGroupType = function (data) {
        $scope.MstGroupType = data;
    };

    $scope.showUser = function (data) {

        $scope.MstGroupType = data;

        $http({
            method: 'POST',
            url: 'api/MstGroupType/MstGroupTypeIsActive',
            data: $scope.MstGroupType,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.MstGroupTypeGet();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.HideUser = function (data) {

        $scope.MstGroupType = data;

        $http({
            method: 'POST',
            url: 'api/MstGroupType/MstGroupTypeIsSuspended',
            data: $scope.MstGroupType,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.MstGroupTypeGet();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});