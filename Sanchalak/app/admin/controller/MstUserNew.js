app.controller('MstUserNewCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams, $filter) {

    $rootScope.pageTitle = "Manage Mst Users";
    $scope.UpdateBtnFlag = false;

    /*Reset MstUserNew*/
    $scope.resetMstUserNew = function () {
        $scope.MstUserNew = {};
        $scope.UpdateBtnFlag = false;
    };

    /*Get UserTypeMaster List*/
    $scope.UserMasterGet = function () {

        $http({
            method: 'POST',
            url: 'api/MstUserNew/UserTypeMasterGet',
            data: $scope.MstUserNew,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.UserTypeList = response.obj;
                //console.log($scope.UserTypeList);
            })
            .error(function (res) {
                alert(res);
            });

    };

    /*Get EmployeeMaster List*/
    $scope.MstEmployeeGetforUser = function () {

        $http({
            method: 'POST',
            url: 'api/MstUserNew/MstEmployeeGetforUser',
            data: $scope.MstUserNew,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.EmployeeList = response.obj;

                /*Start - Function for get data in Search Dropdown list
                $(".js-example-data-array").select2({
                    data: $scope.EmployeeList.FullName
                });
                End - Function for get data in Search Dropdown list*/
            })
            .error(function (res) {
                alert(res);
            });

    };

    /*Get MstUserNew List*/
    $scope.MstUserNewGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstUserNew/MstUserNewGet',
            
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
                    $scope.MstUserNewTableParams = new NgTableParams({}, { dataset: response.obj });
                    $scope.MstUserNewData = response.obj;
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add MstUserNew Data*/
    $scope.MstUserNewAdd = function () {

        if ($scope.MstUserNew.UserTypeId == null || $scope.MstUserNew.UserTypeId == "" || $scope.MstUserNew.UserTypeId == undefined) {

            alert("Please select User Type..!");
        }
        else if ($scope.MstUserNew.UserName == null || $scope.MstUserNew.UserName == "" || $scope.MstUserNew.UserName == undefined ||
            $scope.MstUserNew.Password == null || $scope.MstUserNew.Password == "" || $scope.MstUserNew.Password == undefined ||
            $scope.MstUserNew.MobileNo == null || $scope.MstUserNew.MobileNo == "" || $scope.MstUserNew.MobileNo == undefined ||
            $scope.MstUserNew.EmailId == null || $scope.MstUserNew.EmailId == "" || $scope.MstUserNew.EmailId == undefined ||
            $scope.MstUserNew.DisplayName == null || $scope.MstUserNew.DisplayName == "" || $scope.MstUserNew.DisplayName == undefined)
        {
            alert("Please check all required fields..!");
        }
        else
        {

            $http({
                method: 'POST',
                url: 'api/MstUserNew/MstUserNewAdd',
                data: $scope.MstUserNew,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);                                                                    
                        $scope.MstUserNewGet();
                        $scope.MstUserNew = {};

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };   

    /*Modify MstUserNew Data*/
    $scope.modifyMstUserNewData = function (data) {
      
        //debugger;
        //$('#ddlEmployee').change(function () {
        //    var value = data.EmployeeId;
        //    //console.log(data);
        //     //Set selected 
        //    $('#ddlEmployee').val('number:2');
        //    $('#ddlEmployee').select2().trigger('change');

        //});
        $scope.showFormFlag = true;
        $scope.MstUserNew = data;
        $(window).scrollTop(0);
        $scope.UpdateBtnFlag = true;
    };

    /*Update MstUserNew Data*/
    $scope.MstUserNewEdit = function () {

        if ($scope.MstUserNew.UserName == null || $scope.MstUserNew.UserName == "" || $scope.MstUserNew.UserName == undefined ||
            $scope.MstUserNew.Password == null || $scope.MstUserNew.Password == "" || $scope.MstUserNew.Password == undefined ||
            $scope.MstUserNew.MobileNo == null || $scope.MstUserNew.MobileNo == "" || $scope.MstUserNew.MobileNo == undefined ||
            $scope.MstUserNew.EmailId == null || $scope.MstUserNew.EmailId == "" || $scope.MstUserNew.EmailId == undefined ||
            $scope.MstUserNew.DisplayName == null || $scope.MstUserNew.DisplayName == "" || $scope.MstUserNew.DisplayName == undefined) {
            alert("Please check all required fields..!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstUserNew/MstUserNewEdit',
                data: $scope.MstUserNew,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj); 
                        $scope.resetMstUserNew();
                        $scope.MstUserNewGet();
                        $scope.MstUserNew = {};
                        $scope.showFormFlag = false;
                        $scope.UpdateBtnFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    }; 

    /*Delete MstUserNew Data*/
    $scope.MstUserNewDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstUserNew = data;

            $http({
                method: 'POST',
                url: 'api/MstUserNew/MstUserNewDelete',
                data: $scope.MstUserNew,
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
                        $scope.MstUserNewGet();
                        $scope.MstUserNew = {};
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

});