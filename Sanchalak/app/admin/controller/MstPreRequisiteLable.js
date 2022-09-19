app.controller('MstPreRequisiteLableCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage MstPreRequisiteLable";

    /*Reset MstPreRequisiteType*/
    $scope.resetMstPreRequisiteLable = function () {
        $scope.MstPreRequisiteLable = {};
    };

    /*Get MstPreRequisiteType List*/
    $scope.getMstPreRequisiteLable = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstPreRequisiteLable/MstPreRequisiteLableGet',
            
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
                    $scope.MstPreRequisiteLableTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add MstPreRequisiteType*/
    $scope.addMstPreRequisiteLable = function () {

        if ($scope.MstPreRequisiteLable.PreRequisiteLableName === null || $scope.MstPreRequisiteLable.PreRequisiteLableName === undefined) {
            alert("Enter PreRequisite Lable Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstPreRequisiteLable/MstPreRequisiteLableAdd',
                data: $scope.MstPreRequisiteLable,
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
                        $scope.MstPreRequisiteLable = {};
                        $scope.getMstPreRequisiteLable();
                        $state.go('MstPreRequisiteLableEdit');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify MstPreRequisiteType Data*/
    $scope.modifyMstPreRequisiteLableData = function (data) {
        $scope.MstPreRequisiteLable = data;
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update MstPreRequisiteType*/
    $scope.editMstPreRequisiteLable = function () {
        if ($scope.MstPreRequisiteLable.PreRequisiteLableName === null || $scope.MstPreRequisiteLable.PreRequisiteLableName === undefined) {
            alert("Enter PreRequisite Lable Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstPreRequisiteLable/MstPreRequisiteLableUpdate',
                data: $scope.MstPreRequisiteLable,
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
                        $scope.getMstPreRequisiteLable();                        
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete MstPreRequisiteType*/
    $scope.deleteMstPreRequisiteLable = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstPreRequisiteLable = data;

            $http({
                method: 'POST',
                url: 'api/MstPreRequisiteLable/MstPreRequisiteLableDelete',
                data: $scope.MstPreRequisiteLable,
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
                        $scope.getMstPreRequisiteLable();
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

    /*Add New MstPreRequisiteLable*/
    $scope.newMstPreRequisiteLableAdd = function () {
        $state.go('MstPreRequisiteLableAdd');
    };

    /*Back to Edit Page of MstPreRequisiteLable*/
    $scope.backToList = function () {
        $state.go('MstPreRequisiteLableEdit');
    };

    /*Display MstPreRequisiteType Data*/
    $scope.displayMstPreRequisiteLable = function (data) {
        $scope.MstPreRequisiteLable = data;
    };

    /*Active Enable MstPreRequisiteType*/
    $scope.ShowMstPreRequisiteLable = function (data) {

        $scope.MstPreRequisiteLable = data;

        $http({
            method: 'POST',
            url: 'api/MstPreRequisiteLable/MstPreRequisiteLableIsActive',
            data: $scope.MstPreRequisiteLable,
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
                    $scope.getMstPreRequisiteLable();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable MstPreRequisiteType*/
    $scope.HideMstPreRequisiteLable = function (data) {

        $scope.MstPreRequisiteLable = data;

        $http({
            method: 'POST',
            url: 'api/MstPreRequisiteLable/MstPreRequisiteLableIsSuspended',
            data: $scope.MstPreRequisiteLable,
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
                    $scope.getMstPreRequisiteLable();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});






    

