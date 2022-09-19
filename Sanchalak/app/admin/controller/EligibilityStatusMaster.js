app.controller('EligibilityStatusMasterCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage EligibilityStatusMaster";

    /*Reset EligibilityStatusMaster*/
    $scope.resetEligibilityStatusMaster = function () {
        $scope.EligibilityStatusMaster = {};
    };

    /*Get EligibilityStatusMaster List*/
    $scope.getEligibilityStatusMaster = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/EligibilityStatusMaster/EligibilityStatusMasterGet',
            
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
                    $scope.EligibilityStatusMasterTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add EligibilityStatusMaster*/
    $scope.addEligibilityStatusMaster = function () {

        if ($scope.EligibilityStatusMaster.EligibilityStatus === null || $scope.EligibilityStatusMaster.EligibilityStatus === undefined) {
            alert("Enter Eligibility Status");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/EligibilityStatusMaster/EligibilityStatusMasterAdd',
                data: $scope.EligibilityStatusMaster,
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
                        $scope.EligibilityStatusMaster = {};
                        $scope.getEligibilityStatusMaster();
                        $state.go('EligibilityStatusMasterEdit');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify EligibilityStatusMaster Data*/
    $scope.modifyEligibilityStatusMasterData = function (data) {
        $scope.EligibilityStatusMaster = data;
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update EligibilityStatusMaster*/
    $scope.editEligibilityStatusMaster = function () {
        if ($scope.EligibilityStatusMaster.EligibilityStatus === null || $scope.EligibilityStatusMaster.EligibilityStatus === undefined) {
            alert("Enter Eligibility Status");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/EligibilityStatusMaster/EligibilityStatusMasterUpdate',
                data: $scope.EligibilityStatusMaster,
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
                        $scope.getEligibilityStatusMaster();                        
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete EligibilityStatusMaster*/
    $scope.deleteEligibilityStatusMaster = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.EligibilityStatusMaster = data;

            $http({
                method: 'POST',
                url: 'api/EligibilityStatusMaster/EligibilityStatusMasterDelete',
                data: $scope.EligibilityStatusMaster,
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
                        $scope.getEligibilityStatusMaster();
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

    /*Add New EligibilityStatusMaster*/
    $scope.newEligibilityStatusMasterAdd = function () {
        $state.go('EligibilityStatusMasterAdd');
    };

    /*Back to Edit Page of EligibilityStatusMaster*/
    $scope.backToList = function () {
        $state.go('EligibilityStatusMasterEdit');
    };

    /*Display EligibilityStatusMaster Data*/
    $scope.displayEligibilityStatusMaster = function (data) {
        $scope.EligibilityStatusMaster = data;
    };

    /*Active Enable EligibilityStatusMaster*/
    $scope.ShowEligibilityStatusMaster = function (data) {

        $scope.EligibilityStatusMaster = data;

        $http({
            method: 'POST',
            url: 'api/EligibilityStatusMaster/EligibilityStatusMasterIsActive',
            data: $scope.EligibilityStatusMaster,
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
                    $scope.getEligibilityStatusMaster();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable EligibilityStatusMaster*/
    $scope.HideEligibilityStatusMaster = function (data) {

        $scope.EligibilityStatusMaster = data;

        $http({
            method: 'POST',
            url: 'api/EligibilityStatusMaster/EligibilityStatusMasterIsSuspended',
            data: $scope.EligibilityStatusMaster,
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
                    $scope.getEligibilityStatusMaster();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});






    

