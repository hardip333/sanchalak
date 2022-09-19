app.controller('MstPreRequisiteTypeCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage MstPreRequisiteType";

    /*Reset MstPreRequisiteType*/
    $scope.resetMstPreRequisiteType = function () {
        $scope.MstPreRequisiteType = {};
    };

    /*Get MstPreRequisiteType List*/
    $scope.getMstPreRequisiteType = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstPreRequisiteType/MstPreRequisiteTypeGet',
            
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
                    $scope.MstPreRequisiteTypeTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add MstPreRequisiteType*/
    $scope.addMstPreRequisiteType = function () {

        if ($scope.MstPreRequisiteType.PreRequisiteTypeName === null || $scope.MstPreRequisiteType.PreRequisiteTypeName === undefined) {
            alert("Enter PreRequisite Type Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstPreRequisiteType/MstPreRequisiteTypeAdd',
                data: $scope.MstPreRequisiteType,
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
                        $scope.MstPreRequisiteType = {};
                        $scope.getMstPreRequisiteType();
                        $state.go('MstPreRequisiteTypeEdit');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify MstPreRequisiteType Data*/
    $scope.modifyMstPreRequisiteTypeData = function (data) {
        $scope.MstPreRequisiteType = data;
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update MstPreRequisiteType*/
    $scope.editMstPreRequisiteType = function () {
        if ($scope.MstPreRequisiteType.PreRequisiteTypeName === null || $scope.MstPreRequisiteType.PreRequisiteTypeName === undefined) {
            alert("Enter PreRequisite Type Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstPreRequisiteType/MstPreRequisiteTypeUpdate',
                data: $scope.MstPreRequisiteType,
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
                        $scope.getMstPreRequisiteType();                        
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete MstPreRequisiteType*/
    $scope.deleteMstPreRequisiteType = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstPreRequisiteType = data;

            $http({
                method: 'POST',
                url: 'api/MstPreRequisiteType/MstPreRequisiteTypeDelete',
                data: $scope.MstPreRequisiteType,
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
                        $scope.getMstPreRequisiteType();
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

    /*Add New MstPreRequisiteType*/
    $scope.newMstPreRequisiteTypeAdd = function () {
        $state.go('MstPreRequisiteTypeAdd');
    };

    /*Back to Edit Page of MstPreRequisiteType*/
    $scope.backToList = function () {
        $state.go('MstPreRequisiteTypeEdit');
    };

    /*Display MstPreRequisiteType Data*/
    $scope.displayMstPreRequisiteType = function (data) {
        $scope.MstPreRequisiteType = data;
    };

    /*Active Enable MstPreRequisiteType*/
    $scope.ShowMstPreRequisiteType = function (data) {

        $scope.MstPreRequisiteType = data;

        $http({
            method: 'POST',
            url: 'api/MstPreRequisiteType/MstPreRequisiteTypeIsActive',
            data: $scope.MstPreRequisiteType,
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
                    $scope.getMstPreRequisiteType();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable MstPreRequisiteType*/
    $scope.HideMstPreRequisiteType = function (data) {

        $scope.MstPreRequisiteType = data;

        $http({
            method: 'POST',
            url: 'api/MstPreRequisiteType/MstPreRequisiteTypeIsSuspended',
            data: $scope.MstPreRequisiteType,
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
                    $scope.getMstPreRequisiteType();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});






    

