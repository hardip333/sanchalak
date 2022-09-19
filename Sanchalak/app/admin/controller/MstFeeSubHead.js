app.controller('FSHCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    var tokenCookie = $cookies.get('token');
	if (!tokenCookie) {
		localStorage.clear();
		$state.go('login');
	} 
    $rootScope.pageTitle = "Manage Fee Sub Head";

    $scope.FSHTableParams = new NgTableParams({
    }, {
        dataset: $scope.FSHList
    });

    $scope.resetFSH = function () {
        $scope.FSH = {};
    };

    $scope.getFSHList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstFeeSubHead/MstFeeSubHeadGet',
            data: data,
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
                    $scope.FSHTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getFHList = function () {

        $http({
            method: 'POST',
            url: 'api/MstFeeHead/MstFeeHeadGetwithoutsubhead',
            //data: $scope.FEECONFIG,
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
                    $scope.FHList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addFSH = function () {
        if ($scope.FSH.FeeSubHeadName === null || $scope.FSH.FeeSubHeadName === undefined
            //|| $scope.FSH.FeeSubHeadCode === null || $scope.FSH.FeeSubHeadCode === undefined
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
                url: 'api/MstFeeSubHead/MstFeeSubHeadAdd',
                data: $scope.FSH,
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

                        $scope.FSH = {};
                        $scope.getFSHList();
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

    $scope.cancelFSH = function () {
        $scope.FSH = {};
        $scope.modifyFSHFlag = false;
    };

    $scope.modifyFSHData = function (data) {
        $scope.FSH = data;
        $scope.modifyFSHFlag = true;
        $scope.showFormFlag = true;
    };

    $scope.modifyFSH = function () {

        if ($scope.FSH.FeeSubHeadName === null || $scope.FSH.FeeSubHeadName === undefined
            //|| $scope.FSH.FeeSubHeadCode === null || $scope.FSH.FeeSubHeadCode === undefined
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
                url: 'api/MstFeeSubHead/MstFeeSubHeadEdit',
                data: $scope.FSH,
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
                        $scope.FSH = {};
                        $scope.getFSHList();
                        $scope.modifyFSHFlag = false;
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

    $scope.deleteFSH = function (ev, data) {

        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.FSH = data;
            $http({
                method: 'POST',
                url: 'api/MstFeeSubHead/MstFeeSubHeadDelete',
                data: $scope.FSH,
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
                        $scope.getFSHList();
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

    $scope.showFSH = function (data) {

        $scope.newFSH = data;

        $http({
            method: 'POST',
            url: 'api/MstFeeSubHead/MstFeeSubHeadIsActiveEnable',
            data: $scope.newFSH,
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
                    $scope.getFSHList();


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.hideFSH = function (data) {


        $scope.newFSH = data;

        $http({
            method: 'POST',
            url: 'api/MstFeeSubHead/MstFeeSubHeadIsActiveDisable',
            data: $scope.newFSH,
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
                    $scope.getFSHList();


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.newFSHAdd = function () {
        $state.go('FSHAdd');
    };

    $scope.backToList = function () {
        $state.go('FSHEdit');
    };

    $scope.displayFSH = function (data) {
        $scope.FSH = data;
    };

});