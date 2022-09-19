app.controller('MstGroupCodeCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage MstGroupCode";

    $scope.resetMstGroupCode = function () {
        $scope.MstGroupCode = {};
    }

    $scope.MstGroupCodeGet = function () {
        
        var data = new Object();
        
        $http({
            method: 'POST',
            url: 'api/MstGroupCode/MstGroupCodeGet',
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
                    $scope.MstGroupCodeTableParams = new NgTableParams({
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

    $scope.MstGroupCodeAdd = function () {
        
        //if ($scope.MstGroupCode.GroupCode === null || $scope.MstGroupCode.Groupcode === undefined)
        //{
        //    alert("Enter Group Code");
        //}
        //else {
            
            $http({
                method: 'POST',
                url: 'api/MstGroupCode/MstGroupCodeAdd',
                data: $scope.MstGroupCode,
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
                        $scope.MstGroupCode = {};
                        $scope.MstGroupCodeGet();
                        $state.go('MstGroupCodeEdit');
                        
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        //}
    }

    //$scope.cancelUser = function () {
    //    $scope.users = {};
    //    $scope.modifyUserFlag = false;
    //}

    $scope.modifyMstGroupCode = function (data) {
        
        $scope.showFormFlag = true;
        $scope.MstGroupCode = data;
    }

    $scope.MstGroupCodeEdit = function () {
        //if ($scope.MstGroupCode.GroupCode === null || $scope.MstGroupCode.Groupcode === undefined) {
        //    alert("Enter Group Code");
        //}
        //else {

            $http({
                method: 'POST',
                url: 'api/MstGroupCode/MstGroupCodeUpdate',
                data: $scope.MstGroupCode,
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
                        $scope.MstGroupCodeGet();
                        $scope.modifyUserFlag = false;
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        //}
    };

    $scope.MstGroupCodeDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstGroupCode = data;

            $http({
                method: 'POST',
                url: 'api/MstGroupCode/MstGroupCodeDelete',
                data: $scope.MstGroupCode,
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
                        $scope.MstGroupCodeGet();
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
   
    $scope.MstGroupCodeCancle = function () {

        $scope.MstGroupCode = {};
        $scope.modifyMstGroupCodeFlag = false;
    };
    $scope.newMstGroupCodeAdd = function () {
        $state.go('MstGroupCodeAdd');
    };

    $scope.backToList = function () {
        $state.go('MstGroupCodeEdit');
    };
    $scope.displayMstGroupCode = function (data) {
        $scope.MstGroupCode = data;
    };

    $scope.showUser = function (data) {

        $scope.MstGroupCode = data;

        $http({
            method: 'POST',
            url: 'api/MstGroupCode/MstGroupCodeIsActive',
            data: $scope.MstGroupCode,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.MstGroupCodeGet();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.HideUser = function (data) {

        $scope.MstGroupCode = data;

        $http({
            method: 'POST',
            url: 'api/MstGroupCode/MstGroupCodeIsSuspended',
            data: $scope.MstGroupCode,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.MstGroupCodeGet();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});






    

