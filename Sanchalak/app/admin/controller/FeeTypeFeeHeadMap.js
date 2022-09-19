app.controller('FTFHMCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Manage Fee Type Fee Head Map";

    $scope.FTFHMTableParams = new NgTableParams({
    }, {
        dataset: $scope.FTFHMList
    });

    $scope.resetFTFHM = function () {
        $scope.FTFHM = {};
        $scope.FH = {};
    };
    $scope.allcheckflag = false;
    $scope.getFTFHMList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/FeeTypeFeeHeadMap/FeeTypeFeeHeadMapGet',
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
                    $scope.FTFHMTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Academic Year List Get Method */
    $scope.IncAcademicYearListGet = function () {
        //alert("Faculty Details");
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/AcademicYearGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.AcademicList = {};

                    }
                }
                else {
                    $scope.AcademicList = response.obj;
                   
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
                    $scope.FTList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getFHList = function () {

        $http({
            method: 'POST',
            url: 'api/FeeTypeFeeHeadMap/FeeHeadGet',
            data: $scope.FTFHM,
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
                    $scope.FHList = response.obj;
                    $scope.FHflag = true;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getFSHList = function () {

        $http({
            method: 'POST',
            url: 'api/FeeTypeFeeHeadMap/FeeSubHeadGet',
            data: $scope.FTFHM,
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
                    $scope.FSHList = response.obj;
                    var FeeSHList = new Array();
                    for (var i in $scope.FSHList) {
                        var amt1Obj = {};
                        amt1Obj["AddCheck"] = false;
                        amt1Obj["IsEditable"] = $scope.FSHList[i].IsEditable;
                        amt1Obj["Id"] = $scope.FSHList[i].FeeSubHeadId;
                        amt1Obj["FeeHeadId"] = $scope.FSHList[i].FeeHeadId;
                        amt1Obj["FeeSubHeadName"] = $scope.FSHList[i].FeeSubHeadName;
                        amt1Obj["SubHeadChecked"] = $scope.FSHList[i].SubHeadChecked;
                        FeeSHList.push(amt1Obj);
                        if ($scope.FSHList[i].SubHeadChecked == true) {
                            $scope.allcheckflag = true;
                        }
                    }
                    $scope.FTFHM.FeeSHList = FeeSHList;
                    $scope.FSHflag = true;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    
    $scope.allSHcheck = function () {
        if ($scope.FTFHM.AllCheck == true) {
            for (var i in $scope.FTFHM.FeeSHList) {
                $scope.FTFHM.FeeSHList[i].AddCheck = true;
            }
        }
        else {
            for (var i in $scope.FTFHM.FeeSHList) {
                $scope.FTFHM.FeeSHList[i].AddCheck = false;
            }
        }

    };
    $scope.addFTFHM = function () {
        if ($scope.FTFHM.FeeTypeId === null || $scope.FTFHM.FeeTypeId === undefined
            || $scope.FTFHM.AcademicYearId === null || $scope.FTFHM.AcademicYearId === undefined


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
                url: 'api/FeeTypeFeeHeadMap/FeeTypeFeeHeadMapAdd',
                data: $scope.FTFHM,
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
                        $scope.FTFHM = {};
                        $scope.FH = {};
                        $scope.getFTFHMList();
                        $scope.getFHList();
                        $scope.FSHflag = false;
                        $scope.FHflag = false;
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

    $scope.cancelFTFHM = function () {
        $scope.FTFHM = {};
        $scope.FH = {};
        $scope.modifyFTFHMFlag = false;
    };

    $scope.modifyFTFHMData = function (data) {
        $scope.FTFHM = data;
        $scope.modifyFTFHMFlag = true;
        $scope.showFormFlag = true;
    };

    $scope.modifyFTFHM = function () {

        if ($scope.FTFHM.FeeTypeId === null || $scope.FTFHM.FeeTypeId === undefined
            || $scope.FTFHM.FeeHeadId === null || $scope.FTFHM.FeeHeadId === undefined
            
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
                url: 'api/FeeTypeFeeHeadMap/FeeTypeFeeHeadMapEdit',
                data: $scope.FTFHM,
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
                        $scope.FTFHM = {};
                        $scope.getFTFHMList();
                        $scope.modifyFTFHMFlag = false;
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

    $scope.deleteFTFHM = function (ev, data) {

        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.FTFHM = data;
            $http({
                method: 'POST',
                url: 'api/FeeTypeFeeHeadMap/FeeTypeFeeHeadMapDelete',
                data: $scope.FTFHM,
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
                        $scope.getFTFHMList();
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

    $scope.showFTFHM = function (data) {

        $scope.newFTFHM = data;

        $http({
            method: 'POST',
            url: 'api/FeeTypeFeeHeadMap/FeeTypeFeeHeadMapIsActiveEnable',
            data: $scope.newFTFHM,
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
                    $scope.getFTFHMList();


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.hideFTFHM = function (data) {


        $scope.newFTFHM = data;

        $http({
            method: 'POST',
            url: 'api/FeeTypeFeeHeadMap/FeeTypeFeeHeadMapIsActiveDisable',
            data: $scope.newFTFHM,
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
                    $scope.getFTFHMList();


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.newFTFHMAdd = function () {
        $state.go('FTFHMAdd');
    };

    $scope.backToList = function () {
        $state.go('FTFHMEdit');
    };

    $scope.displayFTFHM = function (data) {
        $scope.FTFHM = data;
    };

});