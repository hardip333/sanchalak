app.controller('InstructionMediumCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage InstructionMedium";

    /*Reset InstructionMedium*/
    $scope.resetInstructionMedium = function () {
        $scope.InstructionMedium = {};
    };

    /*Get InstructionMedium*/
    $scope.getInstructionMedium = function () {

        var data = new Object();

        $http({
            method: 'GET',
            url: 'api/InstructionMedium/InstructionMediumGet',
            
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
                    $scope.InstructionMediumTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };    

    /*Add InstructionMedium*/
    $scope.addInstructionMedium = function () {
        if ($scope.InstructionMedium.InstructionMediumName === null || $scope.InstructionMedium.InstructionMediumName === undefined) { alert("Enter All Fields"); }
        else {

            $http({
                method: 'POST',
                url: 'api/InstructionMedium/InstructionMediumAdd',
                data: $scope.InstructionMedium,
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
                        $scope.InstructionMedium = {};
                        $scope.getInstructionMedium();
                        $state.go('UpdateInstructionMedium');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };    

    /*Modify InstructionMedium data*/
    $scope.modifyInstructionMediumData = function (data) {
        $scope.InstructionMedium = data;
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update InstructionMedium*/
    $scope.editInstructionMedium = function () {
        if ($scope.InstructionMedium.InstructionMediumName === null || $scope.InstructionMedium.InstructionMediumName === undefined) { alert("Enter All Fields"); }
        else {
            $http({
                method: 'POST',
                url: 'api/InstructionMedium/InstructionMediumUpdate',
                data: $scope.InstructionMedium,
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
                        $scope.showFormFlag = false;
                        $scope.getInstructionMedium();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete InstructionMedium*/
    $scope.deleteInstructionMedium = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.InstructionMedium = data;

            $http({
                method: 'POST',
                url: 'api/InstructionMedium/InstructionMediumDelete',
                data: $scope.InstructionMedium,
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
                        $scope.getInstructionMedium();
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

    /*Add New InstructionMedium*/
    $scope.newInstructionMediumAdd = function () {
        $state.go('instructionMediumAdd');
    };

    /*Back to Edit Page of InstructionMedium*/
    $scope.backToList = function () {
        $state.go('instructionMediumEdit');
    };

    /*Display InstructionMedium Data*/
    $scope.displayInstructionMedium = function (data) {
        $scope.InstructionMedium = data;
    };

    /*Active Enable InstructionMedium*/
    $scope.ShowInstructionMedium = function (data) {

        $scope.newInstructionMedium = data;

        $http({
            method: 'POST',
            url: 'api/InstructionMedium/MstInstructionMediumIsActive',
            data: $scope.newInstructionMedium,
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
                    $scope.getInstructionMedium();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable InstructionMedium*/
    $scope.HideInstructionMedium = function (data) {

        $scope.newInstructionMedium = data;

        $http({
            method: 'POST',
            url: 'api/InstructionMedium/MstInstructionMediumIsSuspended',
            data: $scope.newInstructionMedium,
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
                    $scope.getInstructionMedium();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});








