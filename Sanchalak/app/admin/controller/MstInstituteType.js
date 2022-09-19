app.controller('MstInstituteTypeCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage MstInstituteType";

    /*Reset MstInstituteType*/
    $scope.resetMstInstituteType = function () {
        $scope.MstInstituteType = {};
    };

    /*Get MstInstituteType List*/
    $scope.getMstInstituteType = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstInstituteType/MstInstituteTypeGet',
            
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
                    $scope.MstInstituteTypeTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add InstituteTypeName*/
    $scope.addMstInstituteType = function () {

        if ($scope.MstInstituteType.InstituteTypeName === null || $scope.MstInstituteType.InstituteTypeName === undefined) {
            alert("Enter InstituteType Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstInstituteType/MstInstituteTypeAdd',
                data: $scope.MstInstituteType,
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
                        $scope.MstInstituteType = {};
                        $scope.getMstInstituteType();
                        $state.go('MstInstituteTypeEdit');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify InstituteTypeName Data*/
    $scope.modifyMstInstituteTypeData = function (data) {
        $scope.MstInstituteType = data;
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update MstInstituteType*/
    $scope.editMstInstituteType = function () {
        if ($scope.MstInstituteType.InstituteTypeName === null || $scope.MstInstituteType.InstituteTypeName === undefined) {
            alert("Enter InstituteType Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstInstituteType/MstInstituteTypeUpdate',
                data: $scope.MstInstituteType,
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
                        $scope.getMstInstituteType();                        
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete MstInstituteType*/
    $scope.deleteMstInstituteType = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstInstituteType = data;

            $http({
                method: 'POST',
                url: 'api/MstInstituteType/MstInstituteTypeDelete',
                data: $scope.MstInstituteType,
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
                        $scope.getMstInstituteType();
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

    /*Add New MstInstituteType*/
    $scope.newMstInstituteTypeAdd = function () {
        $state.go('MstInstituteTypeAdd');
    };

    /*Back to Edit Page of MstInstituteType*/
    $scope.backToList = function () {
        $state.go('MstInstituteTypeEdit');
    };

    /*Display MstInstituteType Data*/
    $scope.displayMstInstituteType = function (data) {
        $scope.MstInstituteType = data;
    };

    /*Active Enable MstInstituteType*/
    $scope.ShowMstInstituteType = function (data) {

        $scope.MstInstituteType = data;

        $http({
            method: 'POST',
            url: 'api/MstInstituteType/MstInstituteTypeIsActive',
            data: $scope.MstInstituteType,
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
                    $scope.getMstInstituteType();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable MstInstituteType*/
    $scope.HideMstInstituteType = function (data) {

        $scope.MstInstituteType = data;

        $http({
            method: 'POST',
            url: 'api/MstInstituteType/MstInstituteTypeIsSuspended',
            data: $scope.MstInstituteType,
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
                    $scope.getMstInstituteType();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});






    

