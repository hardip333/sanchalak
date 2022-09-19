app.controller('UserNotificationCtrl', function ($scope, $http, $window, $filter,$rootScope, $state, $cookies, $localStorage,Upload, $mdDialog, NgTableParams, $localStorage) {
	var tokenCookie = $cookies.get('token');
	if (!tokenCookie) {
		localStorage.clear();
		$state.go('login');
	} 
    $rootScope.pageTitle = "Manage UserNotification";

    var today = new Date();
    var date = today.getDate();
    var month = today.getMonth() + 1;
    var year = today.getFullYear();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();

    $scope.UserNotification = {

        NotificationUserType1:false,
        NotificationUserType2: false,
        NotificationUserType3: false
    };

    

    $scope.resetUserNotification = function () {
        $scope.UserNotification = {};
    };


    $scope.UserNotificationlList = [
        {
            id: '1',
            name: 'Both'
        }, {
            id: '2',
            name: 'Android'
        }, {
            id: '2',
            name: ' Web'
        }];
    
    $scope.getUserNotification = function () {
        //debugger;
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/UserNotification/UserNotificationGet',
            data: data,
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
                    $scope.UserNotificationTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getUserNotification();

    $scope.resetUser = function () {
        $scope.user = {};
    };

    $scope.getFacultyList = function () {

        $http({
            method: 'POST',
            url: 'api/UserNotification/FacultyGet',
            data: $scope.UserNotification,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getIncProgrammeInstancePartTerm = function () {

        $http({
            method: 'POST',
            url: 'api/UserNotification/ProgPartTermGetByFacultyId',
            data: $scope.UserNotification,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgInstPartTermList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgrammeList = function () {
        //alert("Hii");
        $http({
            method: 'POST',
            url: 'api/UserNotification/MstProgrammeGetByFacultyId',
            data: $scope.UserNotification,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    }; 

    $scope.getNotificationType = function () {
        //alert("Hii");
        $http({
            method: 'POST',
            url: 'api/UserNotification/NotificationTypeGet',
            data: $scope.UserNotification,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.NTList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.addUserNotification = function () {
        //debugger;
        //$scope.UserNotification = $scope.UserNotification.NotificationUserType1;
        console.log($scope.UserNotification);
        if ($scope.UserNotification.FacultyId === null || $scope.UserNotification.FacultyId === undefined ||
            $scope.UserNotification.IncInstancePartTermId === null || $scope.UserNotification.IncInstancePartTermId === undefined ||
            $scope.UserNotification.ProgrammeId === null || $scope.UserNotification.ProgrammeId === undefined ||
            $scope.UserNotification.NotificationTypeId === null || $scope.UserNotification.NotificationTypeId === undefined ||
            $scope.UserNotification.NotificationDescription === null || $scope.UserNotification.NotificationDescription === undefined ||
            $scope.UserNotification.NotificationFor === null || $scope.UserNotification.NotificationFor === undefined) { alert("Enter All Fields"); }
        else {

            //var obj = { "FacultyId": FacultyId, "ProgrammeId": ProgrammeId, "IncInstancePartTermId": IncInstancePartTermId, "InstituteQueryImageDoc": InstituteQueryImageDoc}
            $scope.UserNotification.NotificationFile = $scope.UserNotificationImageDoc;
            console.log($scope.UserNotificationImageDoc);
                $http({
                    method: 'POST',
                    url: 'api/UserNotification/UserNotificationAdd',
                    data: $scope.UserNotification,
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
                            $scope.UserNotification = {};
                            $scope.getUserNotification();
                            $state.go("UserNotificationEdit");
                                                
                        }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
            }
    };       

    $scope.modifyUserNotification = function (data) {
        //debugger;   
        $scope.showFormFlag = true;
        var dob = data.NotificationStartDate;
        var ProperDate1 = dob.slice(6);
        var ProperDate2 = ProperDate1.slice(0, ProperDate1.length - 2);
        var ProperDate3 = parseInt(ProperDate2);
        var item = ProperDate3;
        var FinalDate = new Date(item);
        //alert(FinalDate);
        $scope.secondDate = $filter('date')(FinalDate, "yyyy-MM-dd");
        //alert($scope.secondDate);

        var dob1 = data.NotificationEndDate;
        var ProperDate4 = dob1.slice(6);
        var ProperDate5 = ProperDate4.slice(0, ProperDate4.length - 2);
        var ProperDate6 = parseInt(ProperDate5);
        var item1 = ProperDate6;
        var FinalDate1 = new Date(item1);
        $scope.secondDate1 = $filter('date')(FinalDate1, "yyyy-MM-dd"); 
        $scope.UserNotification = data;     
        $scope.getFacultyList();
        $scope.getProgrammeList();
        $scope.getIncProgrammeInstancePartTerm();
        $scope.getNotificationType();
        
            
        };  

    $scope.updateUserNotification = function () {
        if ($scope.UserNotification.FacultyId === null || $scope.UserNotification.FacultyId === undefined ||
            $scope.UserNotification.IncInstancePartTermId === null || $scope.UserNotification.IncInstancePartTermId === undefined ||
            $scope.UserNotification.ProgrammeId === null || $scope.UserNotification.ProgrammeId === undefined ||
            $scope.UserNotification.NotificationTypeId === null || $scope.UserNotification.NotificationTypeId === undefined ||
            $scope.UserNotification.NotificationDescription === null || $scope.UserNotification.NotificationDescription === undefined ||
            $scope.UserNotification.NotificationFor === null || $scope.UserNotification.NotificationFor === undefined) { alert("Enter All Fields"); }
        else {

            //alert("Update Data");
            //debugger;
            //$scope.NotificationFile = $scope.UserNotificationImageDoc;
            //console.log($scope.UserNotificationImageDoc);
            $http({
                method: 'POST',
                url: 'api/UserNotification/UserNotificationUpdate',
                data: $scope.UserNotification,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        // $scope.newUser = {};
                        $scope.showFormFlag = false;
                        $scope.getUserNotification();

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.UserNotificationDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.UserNotification = data;

            $http({
                method: 'POST',
                url: 'api/UserNotification/UserNotificationDelete',
                data: $scope.UserNotification,
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
                        alert("your data deleted successfully");
                        $scope.getUserNotification();
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

    $scope.UserNotificationCancle = function () {

        $scope.UserNotification = {};
        $scope.modifyUserNotificationFlag = false;
    };
    
    $scope.newUserNotificationAdd = function () {
        $state.go('UserNotificationAdd');
    };

    $scope.backToList = function () {
        $state.go('UserNotificationEdit');
    };

    $scope.displayUserNotification = function (data) {
        $scope.UserNotification = data;
    };

    /*Active Enable ExamEventMaster*/
    $scope.ShowUserNotification = function (data) {

        $scope.UserNotification = data;

        $http({
            method: 'POST',
            url: 'api/UserNotification/UserNotificationIsActive',
            data: $scope.UserNotification,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getUserNotification();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable ExamEventMaster*/
    $scope.HideUserNotification = function (data) {

        $scope.UserNotification = data;

        $http({
            method: 'POST',
            url: 'api/UserNotification/UserNotificationIsSuspended',
            data: $scope.UserNotification,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getUserNotification();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    //Uploading Document Method
    $scope.UploadUserNotification = function ($files) {
        //debugger;
        $scope.SelectedFiles = $files;
        // alert($scope.SelectedFiles[0].name);
        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop(); 
        // if ($localStorage.AppObject.AppId) {
        //$scope.UserNotificationImageDoc = "UserNotificationImageDoc" + "_" + date + "_" + month + "_" + year + "_" + h + '' + m + '' + s + fileExtension;
        $scope.UserNotificationImageDoc = "UserNotificationImageDoc" + fileExtension;
        $cookies.put("UserNotificationImageDoc", $scope.UserNotificationImageDoc);
        //  }



        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {
            Upload.upload({
                url: 'api/UserNotification/UploadUserNotificationDocument',
                data: {
                    files: $scope.SelectedFiles
                }
            }).then(function (response) {
                $scope.Result = response.data;
            }, function (response) {
                if (response.status > 0) {
                    var errorMsg = response.status + ': ' + response.data;
                    alert(errorMsg);
                }
            }, function (evt) {
                var element = angular.element(document.querySelector('#dvProgress'));
                $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
            });
        }
    };

    $scope.selectFile = function () {
        $("#file").click();
    }
    $scope.fileNameChaged = function () {
        alert("select file");
    }

});