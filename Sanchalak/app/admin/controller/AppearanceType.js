app.controller('AppearanceTypeMasterCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage AppearanceTypeMaster";

    /*Reset AppearanceType*/
    $scope.resetAppearanceTypeMaster = function () {
        $scope.AppearanceTypeMaster = {};
    };

    /*Get AppearanceType List*/
    $scope.getAppearanceTypeMaster = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/AppearanceTypeMaster/AppearanceTypeGet',
            
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
                    $scope.AppearanceTypeMasterTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add AppearanceType*/
    $scope.addAppearanceTypeMaster = function () {

        if ($scope.AppearanceTypeMaster.AppearanceType === null || $scope.AppearanceTypeMaster.AppearanceType === undefined) {
            alert("Enter AppearanceType Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/AppearanceTypeMaster/AppearanceTypeAdd',
                data: $scope.AppearanceTypeMaster,
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
                        $scope.AppearanceTypeMaster = {};
                        $scope.getAppearanceTypeMaster();
                        $state.go('AppearanceTypeEdit');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify AppearanceType Data*/
    $scope.modifyAppearanceTypeMasterData = function (data) {
        $scope.AppearanceTypeMaster = data;
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update AppearanceType*/
    $scope.editAppearanceTypeMaster = function () {
        if ($scope.AppearanceTypeMaster.AppearanceType === null || $scope.AppearanceTypeMaster.AppearanceType === undefined) {
            alert("Enter AppearanceType Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/AppearanceTypeMaster/AppearanceTypeUpdate',
                data: $scope.AppearanceTypeMaster,
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
                        $scope.getAppearanceTypeMaster();                        
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete AppearanceType*/
    $scope.deleteAppearanceTypeMaster = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.AppearanceType = data;

            $http({
                method: 'POST',
                url: 'api/AppearanceTypeMaster/AppearanceTypeDelete',
                data: $scope.AppearanceTypeMaster,
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
                        $scope.getAppearanceTypeMaster();
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

    /*Add New AppearanceType*/
    $scope.newAppearanceTypeMasterAdd = function () {
        $state.go('AppearanceTypeAdd');
    };

    /*Back to Edit Page of AppearanceType*/
    $scope.backToList = function () {
        $state.go('AppearanceTypeEdit');
    };

    /*Display AppearanceType Data*/
    $scope.displayAppearanceTypeMaster = function (data) {
        $scope.AppearanceTypeMaster = data;
    };

    /*Active Enable AppearanceType*/
    $scope.ShowAppearanceTypeMaster = function (data) {

        $scope.AppearanceTypeMaster = data;

        $http({
            method: 'POST',
            url: 'api/AppearanceTypeMaster/AppearanceTypeIsActive',
            data: $scope.AppearanceTypeMaster,
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
                    $scope.getAppearanceTypeMaster();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable AppearanceType*/
    $scope.HideAppearanceTypeMaster = function (data) {

        $scope.AppearanceTypeMaster = data;

        $http({
            method: 'POST',
            url: 'api/AppearanceTypeMaster/AppearanceTypeIsSuspended',
            data: $scope.AppearanceTypeMaster,
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
                    $scope.getAppearanceTypeMaster();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});






    

