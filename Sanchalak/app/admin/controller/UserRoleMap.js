app.controller('UserRoleMapCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams, $filter) {

    $rootScope.pageTitle = "Manage User Role Map";
    $scope.UpdateBtnFlag = false;

    /*Reset UserRoleMap*/
    $scope.resetUserRoleMap = function () {
        $scope.UserRoleMap = {};
        $scope.UpdateBtnFlag = false;
    };

    /*Get MstUser List*/
    $scope.MstUserGetforMap = function () {

        $http({
            method: 'POST',
            url: 'api/UserRoleMap/MstUserGetforMap',
            data: $scope.UserRoleMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.MstUserList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /* Get MstRole List */
    $scope.MstRoleGet = function () {

        $http({
            method: 'POST',
            url: 'api/UserRoleMap/MstRoleGet',
            data: $scope.UserRoleMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.RoleList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get MstDesignation List*/
    $scope.MstDesignationGet = function () {

        $http({
            method: 'POST',
            url: 'api/UserRoleMap/MstDesignationGet',
            data: $scope.UserRoleMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.MstDesignationList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get MstUniversitySection List*/
    $scope.MstUniversitySectionGet = function () {

        $http({
            method: 'POST',
            url: 'api/UserRoleMap/MstUniversitySectionGet',
            data: $scope.UserRoleMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.MstUniversitySectionList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Faculty List*/
    $scope.getFacultyById = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGet',
            data: $scope.UserRoleMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacultyList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Institute List*/
    $scope.getPostInstitutebyFaculty = function () {

        var FacultyId = { FacultyId: $scope.UserRoleMap.FacultyId };

        $http({
            method: 'POST',
            url: 'api/PostConfigurationAdmission/PostInstituteGetbyFaculty',
            data: FacultyId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartTermByInstituteList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get MstDepartmentGetForMap List*/
    $scope.MstDepartmentGetForMap = function () {

        $http({
            method: 'POST',
            url: 'api/UserRoleMap/MstDepartmentGetForMap',
            data: $scope.UserRoleMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.DepartmentList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /* Get MstInstitute List */
    $scope.MstInstituteGet = function () {

        $http({
            method: 'POST',
            url: 'api/UserRoleMap/MstInstituteGet',
            data: $scope.UserRoleMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.MstInstituteList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get UserRoleMap List*/
    $scope.UserRoleMapGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/UserRoleMap/UserRoleMapGet',
            
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
                    $scope.UserRoleMapTableParams = new NgTableParams({}, { dataset: response.obj });
                    $scope.UserRoleMapData = response.obj;
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add UserRoleMap Data*/
    $scope.UserRoleMapAdd = function () {
        //debugger;
        //alert($scope.UserRoleMap.UserId);
        //alert($scope.UserRoleMap.RoleId);
        //alert($scope.UserRoleMap.DesignationId);
        //alert($scope.UserRoleMap.Tennure);
        //alert($scope.UserRoleMap.StartDate);
        //alert($scope.UserRoleMap.EndDate);
        //alert($scope.UserRoleMap.UserLevelType);
        //alert($scope.UserRoleMap.UniversitySectionId);
        //alert($scope.UserRoleMap.FacultyId);
        //alert($scope.UserRoleMap.InstituteId);
        //alert($scope.UserRoleMap.DepartmentId);

        if ($scope.UserRoleMap.UserId == null || $scope.UserRoleMap.UserId == "" || $scope.UserRoleMap.UserId == undefined) {

            alert("Please select User..!");
        }
        else if ($scope.UserRoleMap.RoleId == null || $scope.UserRoleMap.RoleId == "" || $scope.UserRoleMap.RoleId == undefined) {

            alert("Please select Role..!");
        }
        else if ($scope.UserRoleMap.DesignationId == null || $scope.UserRoleMap.DesignationId == "" || $scope.UserRoleMap.DesignationId == undefined) {

            alert("Please select Designation..!");
        }
        else if ($scope.UserRoleMap.Tennure == null || $scope.UserRoleMap.Tennure == "" || $scope.UserRoleMap.Tennure == undefined) {

            alert("Please select Tennure Period..!");
        }
        else if (($scope.UserRoleMap.Tennure == 'True') && ($scope.UserRoleMap.StartDate == null || $scope.UserRoleMap.StartDate == "" || $scope.UserRoleMap.StartDate == undefined)) {
            alert("Please select Start Date..!");
        }
        else if (($scope.UserRoleMap.Tennure == 'True') && ($scope.UserRoleMap.EndDate == null || $scope.UserRoleMap.EndDate == "" || $scope.UserRoleMap.EndDate == undefined)) {
            alert("Please select End Date..!");
        }
        else if ($scope.UserRoleMap.UserLevelType == null || $scope.UserRoleMap.UserLevelType == "" || $scope.UserRoleMap.UserLevelType == undefined) {

            alert("Please Assign User..!");
        }
        else if (($scope.UserRoleMap.UserLevelType == 'University') && ($scope.UserRoleMap.UniversitySectionId == null || $scope.UserRoleMap.UniversitySectionId == "" || $scope.UserRoleMap.UniversitySectionId == undefined)) {
            alert("Please select University Section..!");
        }
        else if (($scope.UserRoleMap.UserLevelType == 'Faculty' || $scope.UserRoleMap.UserLevelType == 'Department') &&
            ($scope.UserRoleMap.FacultyId == null || $scope.UserRoleMap.FacultyId == "" || $scope.UserRoleMap.FacultyId == undefined)) {

            alert("Please select Faculty..!");
        }
        else if (($scope.UserRoleMap.UserLevelType == 'Institute'|| $scope.UserRoleMap.UserLevelType == 'Department') &&
            ($scope.UserRoleMap.InstituteId == null || $scope.UserRoleMap.InstituteId == "" || $scope.UserRoleMap.InstituteId == undefined)) {

            alert("Please select Institute..!");
        }
        else if (($scope.UserRoleMap.UserLevelType == 'Department') &&
            ($scope.UserRoleMap.DepartmentId == null || $scope.UserRoleMap.DepartmentId == "" || $scope.UserRoleMap.DepartmentId == undefined)) {

            alert("Please select Department..!");
        }
        else {
  
            $http({
                method: 'POST',
                url: 'api/UserRoleMap/UserRoleMapAdd',
                data: $scope.UserRoleMap,
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
                        $scope.UserRoleMapGet();
                        $scope.UserRoleMap = {};

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };   

    /*Modify UserRoleMap Data*/
    $scope.modifyUserRoleMapData = function (data) {

        $scope.showFormFlag = true;
        $scope.UserRoleMap = data;
        $(window).scrollTop(0);
        $scope.MstUniversitySectionGet();
        $scope.getFacultyById();
        $scope.getPostInstitutebyFaculty();
        $scope.MstDepartmentGetForMap();
        $scope.MstInstituteGet();
        $scope.UpdateBtnFlag = true;

        var TennureStartDate = $scope.UserRoleMap.StartDateView.split("-");
        $scope.UserRoleMap.StartDate = new Date(TennureStartDate[2], (TennureStartDate[1] >= 1) ? (TennureStartDate[1] - 1) : TennureStartDate[1], TennureStartDate[0]);

        var TennureEndDate = $scope.UserRoleMap.EndDateView.split("-");
        $scope.UserRoleMap.EndDate = new Date(TennureEndDate[2], (TennureEndDate[1] >= 1) ? (TennureEndDate[1] - 1) : TennureEndDate[1], TennureEndDate[0]);

      
    };

    /*Update UserRoleMap Data*/
    $scope.UserRoleMapEdit = function () {

        if ($scope.UserRoleMap.UserId == null || $scope.UserRoleMap.UserId == "" || $scope.UserRoleMap.UserId == undefined) {

            alert("Please select User..!");
        }
        else if ($scope.UserRoleMap.RoleId == null || $scope.UserRoleMap.RoleId == "" || $scope.UserRoleMap.RoleId == undefined) {

            alert("Please select Role..!");
        }
        else if ($scope.UserRoleMap.DesignationId == null || $scope.UserRoleMap.DesignationId == "" || $scope.UserRoleMap.DesignationId == undefined) {

            alert("Please select Designation..!");
        }
        else if ($scope.UserRoleMap.Tennure == null || $scope.UserRoleMap.Tennure == "" || $scope.UserRoleMap.Tennure == undefined) {

            alert("Please select Tennure Period..!");
        }
        else if (($scope.UserRoleMap.Tennure == 'True') && ($scope.UserRoleMap.StartDate == null || $scope.UserRoleMap.StartDate == "" || $scope.UserRoleMap.StartDate == undefined)) {
            alert("Please select Start Date..!");
        }
        else if (($scope.UserRoleMap.Tennure == 'True') && ($scope.UserRoleMap.EndDate == null || $scope.UserRoleMap.EndDate == "" || $scope.UserRoleMap.EndDate == undefined)) {
            alert("Please select End Date..!");
        }
        else if ($scope.UserRoleMap.UserLevelType == null || $scope.UserRoleMap.UserLevelType == "" || $scope.UserRoleMap.UserLevelType == undefined) {

            alert("Please Assign User..!");
        }
        else if (($scope.UserRoleMap.UserLevelType == 'University') && ($scope.UserRoleMap.UniversitySectionId == null || $scope.UserRoleMap.UniversitySectionId == "" || $scope.UserRoleMap.UniversitySectionId == undefined)) {
            alert("Please select University Section..!");
        }
        else if (($scope.UserRoleMap.UserLevelType == 'Faculty' || $scope.UserRoleMap.UserLevelType == 'Department') &&
            ($scope.UserRoleMap.FacultyId == null || $scope.UserRoleMap.FacultyId == "" || $scope.UserRoleMap.FacultyId == undefined)) {

            alert("Please select Faculty..!");
        }
        else if (($scope.UserRoleMap.UserLevelType == 'Institute' || $scope.UserRoleMap.UserLevelType == 'Department') &&
            ($scope.UserRoleMap.InstituteId == null || $scope.UserRoleMap.InstituteId == "" || $scope.UserRoleMap.InstituteId == undefined)) {

            alert("Please select Institute..!");
        }
        else if (($scope.UserRoleMap.UserLevelType == 'Department') &&
            ($scope.UserRoleMap.DepartmentId == null || $scope.UserRoleMap.DepartmentId == "" || $scope.UserRoleMap.DepartmentId == undefined)) {

            alert("Please select Department..!");
        }
        else {
            $http({
                method: 'POST',
                url: 'api/UserRoleMap/UserRoleMapEdit',
                data: $scope.UserRoleMap,
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
                        $scope.resetUserRoleMap();
                        $scope.UserRoleMapGet();
                        $scope.UserRoleMap = {};
                        $scope.showFormFlag = false;
                        $scope.UpdateBtnFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    }; 

    /*Delete UserRoleMap Data*/
    $scope.UserRoleMapDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.UserRoleMap = data;

            $http({
                method: 'POST',
                url: 'api/UserRoleMap/UserRoleMapDelete',
                data: $scope.UserRoleMap,
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
                        $scope.UserRoleMapGet();
                        $scope.UserRoleMap = {};
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