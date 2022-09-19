app.controller('ApplicationFeeCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Manage Application Fee";
    $scope.APFee = {};
    var newCategorylist = new Array();

    $scope.ApplicationFeeTableParams = new NgTableParams({
    }, {
        dataset: $scope.APFeeList
    });

    $scope.resetAPFee = function () {
        $scope.APFee = {};
    };

    $scope.IncAcademicYearListGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/IncAcademicYear/AcademicYearGet',
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
                    $scope.FTList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    
    $scope.getFCList = function () {
        $scope.FCList = {};
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstApplicationFee/MstApplicationFeeFeeCategoryIdGet',
            data: $scope.APFee,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.FCList = {};
                }
                else {

                   
                    $scope.FCList = response.obj;
                    
                    //for (var i in $scope.FCList) {
                    //    var amt2Obj = {};
                    //    amt2Obj["FeeCategoryId"] = $scope.FCList[i].FeeCategoryId;
                    //    /*amt2Obj["IsInstalmentGiven"] = false;*/
                    //    /*amt2Obj["FeeCat"] = false;*/
                    //    /*amt2Obj["FeeState"] = true;*/
                    //   /* amt2Obj["NoOfInstalment"] = 0;*/
                    //    amt2Obj["FeeCategoryName"] = $scope.FCList[i].FeeCategoryName;

                    //    newCategorylist.push(amt2Obj);
                    //}
                    //$scope.newCategorylist = newCategorylist;
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    //$scope.getFCList = function () {
    //    var data = new Object();

    //    $scope.APFee = {};
    //    $scope.APFee.AcademicYearId = $localStorage.APFee.AcademicYearId;
    //    $scope.APFee.FeeTypeId = $localStorage.APFee.FeeTypeId;

    //    $http({
    //        method: 'POST',
    //        url: 'api/FeeTypeFeeCategoryMap/FeeCategoryGetbyFeeTypeId',
    //        data: $scope.FEECONFIG,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;
    //            if (response.response_code == "0") {
    //                $state.go('login');
    //            }
    //            if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //            }
    //            else {
    //                $scope.FeeTypeName = $localStorage.FEECONFIG.FeeTypeName;
    //                /*$scope.PartTermShortName = $localStorage.FEECONFIG.PartTermShortName;*/
    //                $scope.FCList = response.obj;
    //                for (var i in $scope.FCList) {
    //                    var amt2Obj = {};
    //                    amt2Obj["FeeCategoryId"] = $scope.FCList[i].FeeCategoryId;
    //                    /*amt2Obj["IsInstalmentGiven"] = false;*/
    //                    /*amt2Obj["FeeCat"] = false;*/
    //                    /*amt2Obj["FeeState"] = true;*/
    //                   /* amt2Obj["NoOfInstalment"] = 0;*/
    //                    amt2Obj["FeeCategoryName"] = $scope.FCList[i].FeeCategoryName;

    //                    newCategorylist.push(amt2Obj);
    //                }
    //                $scope.newCategorylist = newCategorylist;
    //                $scope.showtable = false;

    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};
    $scope.MstApplicationFeeGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstApplicationFee/MstApplicationFeeGet',
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
                    $scope.ApplicationFeeTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.APPFeeAdd = function () {
       
       

        if ($scope.APFee.AcademicYearId === null || $scope.APFee.AcademicYearId === undefined
            || $scope.APFee.Amount === null || $scope.APFee.Amount === undefined
             )
        {
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
                url: 'api/MstApplicationFee/MstApplicationFeeAdd',
                data: $scope.APFee,
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

                        $scope.APFee = {};
                        $scope.MstApplicationFeeGet();
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

    $scope.cancelAPFee = function () {
        $scope.APFee = {};
        //$scope.APFee = {};
        $scope.modifyAPFeeFlag = false;
    };

    $scope.modifyAPFeeData = function (data) {
        console.log(data);
        $scope.APFee = data;
        $scope.modifyAPFeeFlag = true;
        $scope.showFormFlag = true;
    };

    $scope.modifyAPFee = function () {
        console.log($scope.APFee.Amount);

        if (($scope.APFee.Amount === null || $scope.APFee.Amount === undefined))
        {
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
                url: 'api/MstApplicationFee/MstApplicationFeeEdit',
                data: $scope.APFee,
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
                        $scope.APFee = {};
                        $scope.modifyAPFeeFlag = false;
                        $scope.showFormFlag = false;
                        $scope.MstApplicationFeeGet();
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

    $scope.deleteAPFee = function (ev, data) {
        
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.APFee = data;
            $http({
                method: 'POST',
                url: 'api/MstApplicationFee/MstApplicationFeeDelete',
                data: $scope.APFee,
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

        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    $scope.newAPFeeAdd = function () {
        $state.go('ApplicationFeeAdd');
    };

    $scope.backToList = function () {
        $state.go('ApplicationFeeEdit');
    };

    $scope.displayAPFee = function (data) {
        $scope.APFee = data;
    };



});