app.controller('MstDesignationCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams, $filter) {

    $rootScope.pageTitle = "Manage Mst Designation";
    $scope.UpdateBtnFlag = false;
    /*Reset MstDeignation*/
    $scope.resetMstDesignation = function () {
        $scope.MstDeignation = {};
        $scope.UpdateBtnFlag = false;
    };

    /*Get MstDesignation List*/
    $scope.MstDesignationGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstDesignation/MstDesignationGet',
            
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
                    $scope.MstDeignationTableParams = new NgTableParams({}, { dataset: response.obj });
                    $scope.MstDesignationData = response.obj;
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add MstDesignation Data*/
    $scope.MstDesignationAdd = function () {

        if ($scope.MstDeignation.DesignationName == null || $scope.MstDeignation.DesignationName == "" || $scope.MstDeignation.DesignationName == undefined) {

            alert("Please Add Designation Name..!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstDesignation/MstDesignationAdd',
                data: $scope.MstDeignation,
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
                        $scope.MstDesignationGet();
                        $scope.MstDeignation = {};

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };   

    /*Modify MstDesignation Data*/
    $scope.modifyMstDesignationData = function (data) {
        $scope.showFormFlag = true;
        $scope.MstDeignation = data;
        $(window).scrollTop(0);
        $scope.UpdateBtnFlag = true;
    };

    /*Update MstDesignation Data*/
    $scope.MstDesignationEdit = function () {

        if ($scope.MstDeignation.DesignationName == null || $scope.MstDeignation.DesignationName == "" || $scope.MstDeignation.DesignationName == undefined ) {

            alert("Please Add Designation Name..!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstDesignation/MstDesignationEdit',
                data: $scope.MstDeignation,
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
                        $scope.resetMstDesignation();
                        $scope.MstDesignationGet();
                        $scope.MstDeignation = {};
                        $scope.showFormFlag = false;
                        $scope.UpdateBtnFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    }; 

    /*Delete MstDesignation Data*/
    $scope.MstDeignationDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstDeignation = data;

            $http({
                method: 'POST',
                url: 'api/MstDesignation/MstDesignationDelete',
                data: $scope.MstDeignation,
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
                        $scope.MstDesignationGet();
                        $scope.MstDeignation = {};
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

});