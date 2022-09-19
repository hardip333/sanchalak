app.controller('loginCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog) {

    $cookies.remove("id");

    $rootScope.showLoading = false;

    $scope.IsOTP = false;
    $scope.IsOTPShow = false;
    $scope.myFunct = function (keyEvent) {
        if (keyEvent.which === 13)
            $scope.login();
    }

    $scope.loginObj = {};
    
    $scope.login = function () {
        $localStorage.$reset();
        $rootScope.showLoading = false;

        if ($scope.loginObj.username === null || $scope.loginObj.username === undefined || $scope.loginObj.username === "" || $scope.loginObj.password === undefined || $scope.loginObj.password === null || $scope.loginObj.password === "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Notification")
                    .textContent("Please enter username and password...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Got it!')
            );
        }
        else {
            $scope.loginData = {};
            //$scope.loginData.username = $scope.loginObj.username + "@msubaroda.ac.in";
            $scope.loginData.username = $scope.loginObj.username;
            $scope.loginData.password = $scope.loginObj.password;
            //Generate OTP
            $scope.generateOTP = function () {
                //var string = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'; 
                var digits = '0123456789';
                let OTP = '';
                for (let i = 0; i < 6; i++) {
                    OTP += digits[Math.floor(Math.random() * 10)];
                }
                return OTP;
            };
            $scope.EncOTP = window.btoa($scope.generateOTP());
            $scope.loginData.OTP = $scope.EncOTP;//$scope.generateOTP();
            $http({
                method: 'POST',
                url: 'api/admin/login',
                data: $scope.loginData,
                headers: { "Content-Type": 'application/json' }
            }).success(function (response) {
                $rootScope.showLoading = false;                   
                if (response.response_code != "200") {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .parent(angular.element(document.querySelector('#popupContainer')))
                            .clickOutsideToClose(true)
                            .title("Notification")
                            .textContent(response.obj)
                            .ariaLabel('Alert Dialog Demo')
                            .ok('Got it!')
                    );
                } else {
                    //debugger;
                    //Pending 
                    if (response.obj.IsOTP == 1) {
                        if (response.obj.MobileNo == null || response.obj.MobileNo == undefined || response.obj.MobileNo == "") {
                            $state.go('login');
                        }
                        else {
                            $scope.OTPVerification = {};
                            $scope.OTPVerification.MobileNo = response.obj.MobileNo;
                            $scope.Verification = {};
                            $scope.Verification.IsOTP = response.obj.IsOTP;
                            $scope.Verification.MobileNo = response.obj.MobileNo;
                            //change here
                            $scope.IsOTPShow = true;
                            //window.atob($scope.EncOTP);
                            $localStorage.id = response.obj.id;
                            $cookies.put("id", response.obj.id);
                            $cookies.put("token", response.token);
                            $cookies.put("userRole", response.obj.userTypeName);
                            $scope.userTypeName = response.obj.userTypeName;

                            $scope.loginData.token = response.token;
                            $scope.loginData.id = response.obj.id;

                            return false;
                        }
                    }
                    else {
                        //alert('login else');
                        $localStorage.id = response.obj.id;
                        $cookies.put("id", response.obj.id);
                        $cookies.put("token", response.token);
                        $cookies.put("userRole", response.obj.userTypeName);
                        $scope.userTypeName = response.obj.userTypeName;

                        $scope.loginData.token = response.token;
                        $scope.loginData.id = response.obj.id;

                        $http({
                            method: 'POST',
                            url: 'api/Role/GetRoleByUserId',
                            data: $scope.loginData,
                            headers: { "Content-Type": 'application/json' }
                        }).success(function (response) {
                            $rootScope.showLoading = false;

                            if (response.response_code == "0") {
                                //For log in 
                            } else if (response.response_code != "200") {

                            } else {
                                $scope.stateCheckNo = "Yes";

                                /*  if (Object.keys(response.obj).length > 1) {
                                      $scope.stateCheckNo = "Yes";
                                  }*/
                                //alert("Test==>" + Object.keys(response.obj).length + "===" + $scope.userTypeName+ "===" + $scope.stateCheckNo );
                                if ($scope.userTypeName == "Students") {
                                    $state.go('users');
                                    $cookies.put('roleId', 4);//For student
                                } else if ($scope.userTypeName == "Employees") {
                                    //  alert($scope.stateCheckNo);
                                    if ($scope.stateCheckNo == "Yes") {
                                        $state.go('EmpDashboard1');
                                    } else {
                                        $state.go('users');
                                    }
                                }

                            }

                        })
                    }
                    //Pending

                    //$rootScope.id = response.obj.id;
                     
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
        }
    };


    $scope.VerifyOTP = function (OTP) {
        //alert('VerifyOTP');
        debugger;
        //$scope.DecOTP = ;
        if (OTP != window.atob($scope.EncOTP)) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Notification")
                    .textContent("Invalid OTP! Try again!")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Got it!')
            );
        }
        else
        {
            
            $http({
                method: 'POST',
                url: 'api/Role/GetRoleByUserId',
                data: $scope.loginData,
                headers: { "Content-Type": 'application/json' }
            }).success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');
                } else if (response.response_code != "200") {
                    $state.go('login');
                } else {
                    $scope.stateCheckNo = "Yes";

                    /*  if (Object.keys(response.obj).length > 1) {
                          $scope.stateCheckNo = "Yes";
                      }*/
                    //alert("Test==>" + Object.keys(response.obj).length + "===" + $scope.userTypeName+ "===" + $scope.stateCheckNo );
                    if ($scope.userTypeName == "Students") {
                        $state.go('users');
                        $cookies.put('roleId', 4);//For student
                    } else if ($scope.userTypeName == "Employees") {
                        //  alert($scope.stateCheckNo);
                        if ($scope.stateCheckNo == "Yes") {
                            $state.go('EmpDashboard1');
                        } else {
                            $state.go('users');
                        }
                    }

                }

            })
        }
    };

});

