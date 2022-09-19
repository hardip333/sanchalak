app.controller('FHCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    var tokenCookie = $cookies.get('token');
	if (!tokenCookie) {
		localStorage.clear();
		$state.go('login');
	} 
    $rootScope.pageTitle = "Manage Fee Head";
    $scope.checkflag = true;

    $scope.FHTableParams = new NgTableParams({
    }, {
        dataset: $scope.FHList
    });

    $scope.resetFH = function () {
        $scope.FH = {};
    };

    $scope.getFHList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstFeeHead/MstFeeHeadGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.FHTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addFH = function () {
        if ($scope.FH.FeeHeadName === null || $scope.FH.FeeHeadName === undefined
            //|| $scope.FH.FeeHeadCode === null || $scope.FH.FeeHeadCode === undefined
        ) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please complete the form before Click...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstFeeHead/MstFeeHeadAdd',
                data: $scope.FH,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {

                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {

                        $scope.FH = {};
                        $scope.getFHList();
                        $mdDialog.show(
                            $mdDialog.alert()
                                .parent(angular.element(document.querySelector('#popupContainer')))
                                .clickOutsideToClose(true)
                                .title("Message")
                                .textContent(response.obj)
                                .ariaLabel('Alert Dialog Demo')
                                .ok('Okay!')
                        );
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };
    $scope.checksh = function () {
        if ($scope.FH.HasSubHead == false) {
            $scope.checkflag = true;
        }
        else {
            $scope.checkflag = false;
        }
    };

    $scope.cancelFH = function () {
        $scope.FH = {};
        $scope.modifyFHFlag = false;
    };

    $scope.modifyFHData = function (data) {
        $scope.FH = data;
        $scope.modifyFHFlag = true;
        $scope.showFormFlag = true;
    };

    $scope.modifyFH = function () {

        if ($scope.FH.FeeHeadName === null || $scope.FH.FeeHeadName === undefined
            //|| $scope.FH.FeeHeadCode === null || $scope.FH.FeeHeadCode === undefined
        ) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please complete the form before Click...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstFeeHead/MstFeeHeadEdit',
                data: $scope.FH,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.FH = {};
                        $scope.getFHList();
                        $scope.modifyFHFlag = false;
                        $scope.showFormFlag = false;
                        $mdDialog.show(
                            $mdDialog.alert()
                                .parent(angular.element(document.querySelector('#popupContainer')))
                                .clickOutsideToClose(true)
                                .title("Message")
                                .textContent(response.obj)
                                .ariaLabel('Alert Dialog Demo')
                                .ok('Okay!')
                        );
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.deleteFH = function (ev, data) {

        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.FH = data;
            $http({
                method: 'POST',
                url: 'api/MstFeeHead/MstFeeHeadDelete',
                data: $scope.FH,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    }

                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.getFHList();
                        $mdDialog.show(
                            $mdDialog.alert()
                                .parent(angular.element(document.querySelector('#popupContainer')))
                                .clickOutsideToClose(true)
                                .title("Message")
                                .textContent(response.obj)
                                .ariaLabel('Alert Dialog Demo')
                                .ok('Okay!')
                        );
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

    $scope.showFH = function (data) {

        $scope.newFH = data;

        $http({
            method: 'POST',
            url: 'api/MstFeeHead/MstFeeHeadIsActiveEnable',
            data: $scope.newFH,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getFHList();


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.hideFH = function (data) {


        $scope.newFH = data;

        $http({
            method: 'POST',
            url: 'api/MstFeeHead/MstFeeHeadIsActiveDisable',
            data: $scope.newFH,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getFHList();


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.newFHAdd = function () {
        $state.go('FHAdd');
    };

    $scope.backToList = function () {
        $state.go('FHEdit');
    };

    $scope.displayFH = function (data) {
        $scope.FH = data;
    };

});
