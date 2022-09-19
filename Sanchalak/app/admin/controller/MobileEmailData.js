app.controller('MobileEmailDataCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, $localStorage, NgTableParams) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Mobile and Email Change";
    $scope.showData = false;
    

    $scope.test = function () {
        alert("sssssssssssssssss");
    };


    var InstPartList = [];

    $scope.InstPartList1 = {}; 

    $scope.SubmitApplicationId = function () {
        if ($scope.MobileEmailData.ApplicationId == null || $scope.MobileEmailData.ApplicationId === undefined) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Enter Application ID")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );

        }
        else {
            $http({
                method: 'POST',
                url: 'api/MobileEmailData/MobileEmailDataGet',
                data: $scope.MobileEmailData,
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
                    if (response.response_code == "201") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        $scope.MEDList = response.obj[0];
                        $scope.showData = true;
                    }

                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };


    
    /*Edit Disable MobileEmailDataEdit*/
    $scope.updateMobileEmailData = function () {
        //debugger;
        
        if ($scope.MobileEmailData.ApplicationId == 0) {
            $scope.MobileEmailData.ApplicationId = $localStorage.AppId;

        }
       
        //$scope.MobileEmailData.Data1 = $scope.MEDList;
        
        //if ($scope.MobileEmailData.SMobileNo === null || $scope.MobileEmailData.SMobileNo === undefined ||
        //    $scope.MobileEmailData.SEmailId === null || $scope.MobileEmailData.SEmailId === undefined) {
        //    alert("Please Enter Mobile No. and Email Id");
        //}
        //else {

            $http({
                method: 'POST',
                url: 'api/MobileEmailData/MobileEmailDataEdit',
                data: $scope.MEDList,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.SubmitApplicationId();
                        $scope.InstPartList = $scope.MEDList;
                        //$scope.BlockApplicantsFromAdmission.BlockedRemark1 = null;
                        //$scope.flagdisable = true;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        //}


    }

    

    





    

});