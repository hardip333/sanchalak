app.controller('DepartmentCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog,  NgTableParams) {

    $rootScope.pageTitle = "Manage Department";

    /*Reset Department*/
    $scope.resetDept = function () {
        $scope.Department = {};
    };

    /*Get Department List*/
    $scope.getDepartmentList = function () {

        var data = new Object();
        
        $http({
            method: 'POST',
            url: 'api/MstDepartment/MstDepartmentListGet',            
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
                    $scope.DeptTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Get Faculty List*/
    $scope.getFacultyList = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetForDropDown',            
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacultyList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Add Department*/
    $scope.addDepartment = function () {

        if ($scope.Department.FacultyId === null || $scope.Department.FacultyId === undefined ||
            $scope.Department.DepartmentName === null || $scope.Department.DepartmentName === undefined ||
            $scope.Department.DepartmentCode === null || $scope.Department.DepartmentCode === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstDepartment/MstDepartmentAdd',
                data: $scope.Department,
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
                        $scope.Department = {};
                        $scope.getDepartmentList();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };    

    /*Modify Department Data*/
    $scope.modifyDepartmentData = function (data) {
        $scope.showFormFlag = true;
        $scope.Department = data; 
        $(window).scrollTop(0);
    };

    /*Update Department*/
    $scope.editDepartment = function () {

        if ($scope.Department.FacultyId === null || $scope.Department.FacultyId === undefined ||
            $scope.Department.DepartmentName === null || $scope.Department.DepartmentName === undefined ||
            $scope.Department.DepartmentCode === null || $scope.Department.DepartmentCode === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstDepartment/MstDepartmentUpdate',
                data: $scope.Department,
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
                        $scope.Department = {};
                        $scope.getDepartmentList();
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };  

    /*Delete Department*/
    $scope.deleteDepartment = function (ev, data) {
        
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.Department = data;
            $http({
                method: 'POST',
                url: 'api/MstDepartment/MstDepartmentDelete',
                data: $scope.Department,
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
                        $scope.getDepartmentList();
                    }
                })
                .error(function (res) {
                    alert(response.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            // };
        }, function () {
                $scope.status = 'You decided not to delete your data.';
                alert($scope.status);
        });
    };

    /*Active Enable Department*/
    $scope.ShowDepartment = function (data) {
        
        $scope.newDepartment = data;
       
        $http({
            method: 'POST',
            url: 'api/MstDepartment/MstDepartmentIsActiveEnable',
            data: $scope.newDepartment,
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
                    $scope.getDepartmentList();
                  
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
       
    };

    /*Active Disable Department*/
    $scope.HideDepartment = function (data) {

        $scope.newDepartment = data;
       
        $http({
            method: 'POST',
            url: 'api/MstDepartment/MstDepartmentIsActiveDisable',
            data: $scope.newDepartment,
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
                    $scope.getDepartmentList();             

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    /*Display Department Data*/
    $scope.displayDepartment = function (data) {
        $scope.Department = data;
    };

    /*Add New Department*/
    $scope.newDepartmentAdd = function () {
        $state.go('MstDepartmentAdd');
    };

    /*Back to Edit Page of Department*/
    $scope.backToList = function () {
        $state.go('MstDepartmentEdit');
    };

});
