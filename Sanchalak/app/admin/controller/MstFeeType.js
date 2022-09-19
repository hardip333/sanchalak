app.controller('FTCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
	var tokenCookie = $cookies.get('token');
	if (!tokenCookie) {
		localStorage.clear();
		$state.go('login');
	} 
    $rootScope.pageTitle = "Manage Fee Type";

    $scope.FTTableParams = new NgTableParams({
    }, {
        dataset: $scope.FTList
    });

    $scope.resetFT = function () {
        $scope.FT = {};
    };

    $scope.getFTList = function () {

        var data = new Object();
      
        $http({
            method: 'POST',
            url: 'api/MstFeeType/MstFeeTypeGet',
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
                    $scope.FTTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addFT = function () {
        
        
            if ($scope.FT.FeeTypeName === null || $scope.FT.FeeTypeName === undefined
               // || $scope.FT.FeeTypeCode === null || $scope.FT.FeeTypeCode === undefined
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
                url: 'api/MstFeeType/MstFeeTypeAdd',
                data: $scope.FT,
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
                       
                        $scope.FT = {};
                        $scope.getFTList();
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

    $scope.cancelFT = function () {
        $scope.FT = {};
        $scope.modifyFTFlag = false;
    };

    $scope.modifyFTData = function (data) {
        $scope.FT = data;
        $scope.modifyFTFlag = true;
        $scope.showFormFlag = true;
    };

    $scope.modifyFT = function () {
       
        if ($scope.FT.FeeTypeName === null || $scope.FT.FeeTypeName === undefined
            //|| $scope.FT.FeeTypeCode === null || $scope.FT.FeeTypeCode === undefined
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
                url: 'api/MstFeeType/MstFeeTypeEdit',
                data: $scope.FT,
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
                        $scope.FT = {};
                        $scope.getFTList();
                        $scope.modifyFTFlag = false;
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
   
    $scope.deleteFT = function (ev, data) {
       
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.FT = data;
            $http({
                method: 'POST',
                url: 'api/MstFeeType/MstFeeTypeDelete',
                data: $scope.FT,
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
                        $scope.getFTList();
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
           
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    $scope.showFT = function (data) {
       
        $scope.newFT = data;
       
        $http({
            method: 'POST',
            url: 'api/MstFeeType/MstFeeTypeIsActiveEnable',
            data: $scope.newFT,
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
                    $scope.getFTList();
                   

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.hideFT = function (data) {

       
        $scope.newFT = data;
    
        $http({
            method: 'POST',
            url: 'api/MstFeeType/MstFeeTypeIsActiveDisable',
            data: $scope.newFT,
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
                    $scope.getFTList();
                 

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.newFTAdd = function () {
        $state.go('FTAdd');
    };

    $scope.backToList = function () {
        $state.go('FTEdit');
    };

    $scope.displayFT = function (data) {
        $scope.FT = data;
    };

});
