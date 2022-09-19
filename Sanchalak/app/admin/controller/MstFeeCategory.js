app.controller('FCCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    var tokenCookie = $cookies.get('token');
	if (!tokenCookie) {
		localStorage.clear();
		$state.go('login');
    }
    //var CategoryPeriod = {
    
    //}
    $rootScope.pageTitle = "Manage Fee Category";

    $scope.FCTableParams = new NgTableParams({
    }, {
        dataset: $scope.FCList
    });

    $scope.resetFC = function () {
        $scope.FC = {};
    };

    $scope.getFCList = function () {

        var data = new Object();
        
        $http({
            method: 'POST',
            url: 'api/MstFeeCategory/MstFeeCategoryGet',
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
                    $scope.FCTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getFCTList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstFeeCategoryType/MstFeeCategoryTypeGet',
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
                    $scope.FCTList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getEPList = function () {

        var data = new Object();

        $http({
            method: 'GET',
            url: 'api/MstExaminationPattern/MstExaminationPatternGet',
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
                    $scope.EPList = response.obj;


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addFC = function () {
        console.log($scope.EPList);
        if ($scope.FC.FeeCategoryName === null || $scope.FC.FeeCategoryName === undefined
            || $scope.FC.FeeCategoryTypeId === null || $scope.FC.FeeCategoryTypeId === undefined 
            /*|| $scope.FC.FeeCategoryCode === null || $scope.FC.FeeCategoryCode === undefined*/
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
                url: 'api/MstFeeCategory/MstFeeCategoryAdd',
                data: $scope.FC,
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
                        
                        $scope.FC = {};
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

    $scope.cancelFC = function () {
        $scope.FC = {};
        $scope.modifyFCFlag = false;
    };

    $scope.modifyFCData = function (data) {
        $scope.FC = data;
        $scope.modifyFCFlag = true;
        $scope.showFormFlag = true;
    };

    $scope.modifyFC = function () {
        
        if ($scope.FC.FeeCategoryName === null || $scope.FC.FeeCategoryName === undefined
            /*|| $scope.FC.FeeCategoryCode === null || $scope.FC.FeeCategoryCode === undefined*/
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
                url: 'api/MstFeeCategory/MstFeeCategoryEdit',
                data: $scope.FC,
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
                        $scope.FC = {};
                        $scope.getFCList();
                        $scope.modifyFCFlag = false;
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

    $scope.deleteFC = function (ev, data) {
       
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.FC = data;
            $http({
                method: 'POST',
                url: 'api/MstFeeCategory/MstFeeCategoryDelete',
                data: $scope.FC,
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
            // };
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    $scope.showFC = function (data) {
        
        $scope.newFC = data;
        
        $http({
            method: 'POST',
            url: 'api/MstFeeCategory/MstFeeCategoryIsActiveEnable',
            data: $scope.newFC,
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
                    $scope.getFCList();
                    

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.hideFC = function (data) {

        
        $scope.newFC = data;
        
        $http({
            method: 'POST',
            url: 'api/MstFeeCategory/MstFeeCategoryIsActiveDisable',
            data: $scope.newFC,
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
                    $scope.getFCList();
                   

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.newFCAdd = function () {
        $state.go('FCAdd');
    };

    $scope.backToList = function () {
        $state.go('FCEdit');
    };

    $scope.displayFC = function (data) {
        $scope.FC = data;
    };

});
