app.controller('AdminChangePasswordCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Pre Admin Change Password";

    $scope.cardTitle = "Pre Admin Change Password Operation";


    //$scope.rdocumentTableParams = new NgTableParams({
    //}, {
    //        dataset: $scope.rdocument
    //});

    $scope.resetMstUser = function () {
        $scope.User = {};
    }


    $scope.modifyMstUserData = function (data) {
        $scope.showFormFlag = true;
        $scope.User = data;
    };

    //$scope.myFunction = function (myInput) {
    //function myFunction() {
    //    var x = myInput;
    //    if (x.type === "password") {
    //        x.type = "text";
    //    } else {
    //        x.type = "password";
    //    }
    //}

    $scope.editMstUser = function () {
 
        //$scope.newUser.createdById = $rootScope.id;
        if ($scope.User.ExistingPassword == null || $scope.User.ExistingPassword === undefined || $scope.User.ExistingPassword == " "||
            $scope.User.NewPassword == null || $scope.User.NewPassword === undefined || $scope.User.NewPassword == " " ||
        $scope.User.RepeatNewPassword == null || $scope.User.RepeatNewPassword === undefined || $scope.User.RepeatNewPassword == " "
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#MstUser')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Edit...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.User.NewPassword != $scope.User.RepeatNewPassword) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#MstUser')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("New Password and Repeat New Password does not match...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $http({
                method: 'POST',
                url: 'api/MstUser/MstUserPasswordEdit',
                data: $scope.User,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    //  $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $go.state('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            alert(response.obj);
                            $scope.User = {};
                            
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };


});