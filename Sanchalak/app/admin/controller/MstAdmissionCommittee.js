app.controller('MstAdmissionCommitteeCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage MstAdmissionCommittee";

    /*Reset Evaluation*/
    $scope.resetMstAdmissionCommittee = function () {
        $scope.MstAdmissionCommittee = {};
    };

    /*Get MstAdmissionCommittee List*/
    $scope.getMstAdmissionCommittee = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstAdmissionCommittee/MstAdmissionCommitteeGet',
            
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
                    $scope.MstAdmissionCommitteeTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add MstAdmissionCommittee*/
    $scope.addMstAdmissionCommittee = function () {

        if ($scope.MstAdmissionCommittee.CommitteeName === null || $scope.MstAdmissionCommittee.CommitteeName === undefined) {
            alert("Enter Committee Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstAdmissionCommittee/MstAdmissionCommitteeAdd',
                data: $scope.MstAdmissionCommittee,
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
                        $scope.MstAdmissionCommittee = {};
                        $scope.getMstAdmissionCommittee();
                        $state.go('MstAdmissionCommitteeEdit');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify MstAdmissionCommittee Data*/
    $scope.modifyMstAdmissionCommitteeData = function (data) {
        $scope.MstAdmissionCommittee = data;
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update MstAdmissionCommittee*/
    $scope.editMstAdmissionCommittee = function () {
        if ($scope.MstAdmissionCommittee.CommitteeName === null || $scope.MstAdmissionCommittee.CommitteeName === undefined) {
            alert("Enter MstAdmissionCommittee Name");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstAdmissionCommittee/MstAdmissionCommitteeUpdate',
                data: $scope.MstAdmissionCommittee,
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
                        $scope.getMstAdmissionCommittee();                        
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete MstAdmissionCommittee*/
    $scope.deleteMstAdmissionCommittee = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstAdmissionCommittee = data;

            $http({
                method: 'POST',
                url: 'api/MstAdmissionCommittee/MstAdmissionCommitteeDelete',
                data: $scope.MstAdmissionCommittee,
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
                        $scope.getMstAdmissionCommittee();
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

    /*Add New MstAdmissionCommittee*/
    $scope.newMstAdmissionCommitteeAdd = function () {
        $state.go('MstAdmissionCommitteeAdd');
    };

    /*Back to Edit Page of MstAdmissionCommittee*/
    $scope.backToList = function () {
        $state.go('MstAdmissionCommitteeEdit');
    };

    /*Display MstAdmissionCommittee Data*/
    $scope.displayMstAdmissionCommittee = function (data) {
        $scope.MstAdmissionCommittee = data;
    };

    /*Active Enable MstAdmissionCommittee*/
    $scope.ShowMstAdmissionCommittee = function (data) {

        $scope.MstAdmissionCommittee = data;

        $http({
            method: 'POST',
            url: 'api/MstAdmissionCommittee/MstAdmissionCommitteeIsActive',
            data: $scope.MstAdmissionCommittee,
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
                    $scope.getMstAdmissionCommittee();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable MstAdmissionCommittee*/
    $scope.HideMstAdmissionCommittee = function (data) {

        $scope.MstAdmissionCommittee = data;

        $http({
            method: 'POST',
            url: 'api/MstAdmissionCommittee/MstAdmissionCommitteeIsSuspended',
            data: $scope.MstAdmissionCommittee,
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
                    $scope.getMstAdmissionCommittee();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});






    

