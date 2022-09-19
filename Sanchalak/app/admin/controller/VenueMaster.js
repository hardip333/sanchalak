app.controller('VenueMasterCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage VenueMaster";

    /*Reset VenueMaster*/
    $scope.resetVenueMaster = function () {
        $scope.VenueMaster = {};
    };

    /*Get VenueMaster List*/
    $scope.getVenueMaster = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/VenueMaster/VenueMasterGet',
            
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
                    $scope.VenueMasterTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add VenueMaster*/
    $scope.addVenueMaster = function () {

        if ($scope.VenueMaster.VenueName === null || $scope.VenueMaster.VenueName === undefined ||
            $scope.VenueMaster.VenueAddress === null || $scope.VenueMaster.VenueAddress === undefined ||
            $scope.VenueMaster.VenueCapacity === null || $scope.VenueMaster.VenueCapacity === undefined) {
            alert("Enter Venue Master Fields");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/VenueMaster/VenueMasterAdd',
                data: $scope.VenueMaster,
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
                        $scope.VenueMaster = {};
                        $scope.getVenueMaster();
                        $state.go('VenueMasterEdit');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify VenueMaster Data*/
    $scope.modifyVenueMasterData = function (data) {
        $scope.VenueMaster = data;
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update VenueMaster*/
    $scope.editVenueMaster = function () {
        if ($scope.VenueMaster.VenueName === null || $scope.VenueMaster.VenueName === undefined ||
            $scope.VenueMaster.VenueAddress === null || $scope.VenueMaster.VenueAddress === undefined ||
            $scope.VenueMaster.VenueCapacity === null || $scope.VenueMaster.VenueCapacity === undefined) {
            alert("Enter Venue Master Fields");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/VenueMaster/VenueMasterUpdate',
                data: $scope.VenueMaster,
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
                        $scope.getVenueMaster();                        
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete VenueMaster*/
    $scope.deleteVenueMaster = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.VenueMaster = data;

            $http({
                method: 'POST',
                url: 'api/VenueMaster/VenueMasterDelete',
                data: $scope.VenueMaster,
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
                        $scope.getVenueMaster();
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

    /*Add New VenueMaster*/
    $scope.newVenueMasterAdd = function () {
        $state.go('VenueMasterAdd');
    };

    /*Back to Edit Page of VenueMaster*/
    $scope.backToList = function () {
        $state.go('VenueMasterEdit');
    };

    /*Display VenueMaster Data*/
    $scope.displayVenueMaster = function (data) {
        $scope.VenueMaster = data;
    };

    /*Active Enable VenueMaster*/
    $scope.ShowVenueMaster = function (data) {

        $scope.VenueMaster = data;

        $http({
            method: 'POST',
            url: 'api/VenueMaster/VenueMasterIsActive',
            data: $scope.VenueMaster,
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
                    $scope.getVenueMaster();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable VenueMaster*/
    $scope.HideVenueMaster = function (data) {

        $scope.VenueMaster = data;

        $http({
            method: 'POST',
            url: 'api/VenueMaster/VenueMasterIsSuspended',
            data: $scope.VenueMaster,
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
                    $scope.getVenueMaster();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});






    

