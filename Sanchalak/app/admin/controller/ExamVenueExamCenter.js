app.controller('ExamVenueExamCenterCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams, $filter) {

    $rootScope.pageTitle = "Manage ExamVenue ExamCenter";
    $scope.UpdateBtnFlag = false;

    /*Reset ExamVenueExamCenter*/
    $scope.resetExamVenueExamCenter = function () {
        $scope.ExamVenueExamCenter = {};
        $scope.UpdateBtnFlag = false;
    };

    //Get for ExamVenue ExamCenter List
    $scope.ExamVenueExamCenterGetforVenue = function () {

        $http({
            method: 'POST',
            url: 'api/ExamVenueExamCenter/ExamVenueExamCenterGetforVenue',
            data: $scope.ExamVenueExamCenter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamVenueList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get ExamVenueExamCenter List*/
    $scope.ExamVenueExamCenterGet = function () {

        $http({
            method: 'POST',
            url: 'api/ExamVenueExamCenter/ExamVenueExamCenterGet',
            data: $scope.ExamVenueExamCenter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamVenueExamCenterTableParams = new NgTableParams({}, { dataset: response.obj });
                $scope.ExamVenueExamCenterList = response.obj;
                //console.log($scope.UserTypeList);
            })
            .error(function (res) {
                alert(res);
            });

    };

    /*Add ExamVenueExamCenter Data*/
    $scope.ExamVenueExamCenterAdd = function () {

        //$scope.ExamVenueExamCenter.ExamVenueId = $scope.ExamVenueExamCenter.ExamVenue.ExamVenueId;
        if ($scope.ExamVenueExamCenter.ExamVenueId == null || $scope.ExamVenueExamCenter.ExamVenueId == "" || $scope.ExamVenueExamCenter.ExamVenueId == undefined) {

            alert("Please select Exam Venue..!");
        }
        else if ($scope.ExamVenueExamCenter.CenterName == null || $scope.ExamVenueExamCenter.CenterName == "" || $scope.ExamVenueExamCenter.CenterName == undefined) {

            alert("Please select Center Name..!");
        }
        else
        {

            $http({
                method: 'POST',
                url: 'api/ExamVenueExamCenter/ExamVenueExamCenterAdd',
                data: $scope.ExamVenueExamCenter,
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
                        $scope.ExamVenueExamCenterGet();
                        $scope.ExamVenueExamCenterGetforVenue();
                        $scope.ExamVenueExamCenter = {};

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };   

    /*Modify ExamVenueExamCenter Data*/
    $scope.modifyExamVenueExamCenterData = function (data) {
      
        $scope.showFormFlag = true;
        $scope.ExamVenueExamCenter = data;
        $(window).scrollTop(0);
        $scope.UpdateBtnFlag = true;
        $scope.ExamVenueExamCenterGetforVenue();
    };

    /*Update ExamVenueExamCenter Data*/
    $scope.ExamVenueExamCenterEdit = function () {

        if ($scope.ExamVenueExamCenter.ExamVenueId == null || $scope.ExamVenueExamCenter.ExamVenueId == "" || $scope.ExamVenueExamCenter.ExamVenueId == undefined) {

            alert("Please select Exam Venue..!");
        }
        else if ($scope.ExamVenueExamCenter.CenterName == null || $scope.ExamVenueExamCenter.CenterName == "" || $scope.ExamVenueExamCenter.CenterName == undefined) {

            alert("Please select Center Name..!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/ExamVenueExamCenter/ExamVenueExamCenterEdit',
                data: $scope.ExamVenueExamCenter,
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
                        $scope.resetExamVenueExamCenter();
                        $scope.ExamVenueExamCenterGet();
                        $scope.ExamVenueExamCenterGetforVenue();
                        $scope.ExamVenueExamCenter = {};
                        $scope.showFormFlag = false;
                        $scope.UpdateBtnFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    }; 

    /*Delete ExamVenueExamCenter Data*/
    $scope.ExamVenueExamCenterDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.ExamVenueExamCenter = data;

            $http({
                method: 'POST',
                url: 'api/ExamVenueExamCenter/ExamVenueExamCenterDelete',
                data: $scope.ExamVenueExamCenter,
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
                        $scope.ExamVenueExamCenterGet();
                        $scope.ExamVenueExamCenterGetforVenue();
                        $scope.ExamVenueExamCenter = {};
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