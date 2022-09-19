app.controller('FTFCMCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
	var tokenCookie = $cookies.get('token');
	if (!tokenCookie) {
		localStorage.clear();
		$state.go('login');
	} 
    $rootScope.pageTitle = "Manage Fee Type Fee Category Map";

    $scope.FTFCMTableParams = new NgTableParams({
    }, {
        dataset: $scope.FTFCMList
    });

    $scope.resetFTFCM = function () {
        $scope.FTFCM = {};
        $scope.FC = {};
    };

    $scope.getFTFCMList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/FeeTypeFeeCategoryMap/FeeTypeFeeCategoryMapGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.FTFCMTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Get Academic Year List*/
    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/AcademicYearGet',
            //data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.AYList = response.obj;
                   
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getFTList = function () {
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstFeeType/MstFeeTypeGet',
            //data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.c1List = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getFCList = function () {
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstFeeCategory/MstFeeCategoryGet',
            //data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.c2List = response.obj;
                    

                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };    

    $scope.CategoryGet = function () {
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/FeeTypeFeeCategoryMap/FeeCategoryGetbyFeeTypeId',
            data: $scope.FTFCM,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.CategoryList = response.obj;
                    for (var index = 0; index < $scope.CategoryList.length; index++) {
                        for (var k = 0; k < $scope.c2List.length; k++) {
                            if ($scope.c2List[k].Id == $scope.CategoryList[index].FeeCategoryId) {
                                $scope.c2List[k].boolCheck = true;
                                break;
                            }
                        }
                    }
                    $scope.c4List = $scope.c2List;        
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.addFTFCM = function () {
        if ($scope.FTFCM.FeeTypeId === null || $scope.FTFCM.FeeTypeId === undefined
            || $scope.FTFCM.AcademicYearId === null || $scope.FTFCM.AcademicYearId === undefined
            
            
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
                url: 'api/FeeTypeFeeCategoryMap/FeeTypeFeeCategoryMapAdd',
                data: $scope.FTFCM,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    if (response.response_code != "200") {

                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $('input:checkbox').removeAttr('checked');
                        $scope.FTFCM = {};
                        $scope.FC = {};
                        $scope.getFTFCMList();
                        $scope.getFCList();
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

    $scope.cancelFTFCM = function () {
        $scope.FTFCM = {};
        $scope.FC = {};
        $scope.modifyFTFCMFlag = false;
    };

    $scope.modifyFTFCMData = function (data) {
        $scope.FTFCM = data;
        $scope.modifyFTFCMFlag = true;
        $scope.showFormFlag = true;
    };

    $scope.modifyFTFCM = function () {

        if ($scope.FTFCM.FeeTypeId === null || $scope.FTFCM.FeeTypeId === undefined
            || $scope.FTFCM.FeeCategoryId === null || $scope.FTFCM.FeeCategoryId === undefined
            || $scope.FTFCM.FeeCategoryName === null || $scope.FTFCM.FeeCategoryName === undefined
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
                url: 'api/FeeTypeFeeCategoryMap/FeeTypeFeeCategoryMapEdit',
                data: $scope.FTFCM,
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
                        $scope.FTFCM = {};
                        $scope.getFTFCMList();
                        $scope.modifyFTFCMFlag = false;
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

    $scope.deleteFTFCM = function (ev, data) {

        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.FTFCM = data;
            $http({
                method: 'POST',
                url: 'api/FeeTypeFeeCategoryMap/FeeTypeFeeCategoryMapDelete',
                data: $scope.FTFCM,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.getFTFCMList();
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

    $scope.showFTFCM = function (data) {

        $scope.newFTFCM = data;

        $http({
            method: 'POST',
            url: 'api/FeeTypeFeeCategoryMap/FeeTypeFeeCategoryMapIsActiveEnable',
            data: $scope.newFTFCM,
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
                    $scope.getFTFCMList();


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.hideFTFCM = function (data) {


        $scope.newFTFCM = data;

        $http({
            method: 'POST',
            url: 'api/FeeTypeFeeCategoryMap/FeeTypeFeeCategoryMapIsActiveDisable',
            data: $scope.newFTFCM,
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
                    $scope.getFTFCMList();


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.newFTFCMAdd = function () {
        $state.go('FTFCMAdd');
    };

    $scope.backToList = function () {
        $state.go('FTFCMEdit');
    };

    $scope.displayFTFCM = function (data) {
        $scope.FTFCM = data;
    };

});