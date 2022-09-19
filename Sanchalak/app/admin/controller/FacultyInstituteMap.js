app.controller('FacultyInstituteMapCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage FacultyInstituteMap";

    /*Reset FacultyInstituteMap*/
    $scope.resetFacultyInstituteMap = function () {
        $scope.FacultyInstituteMap = {};
    };

    /*Get FacultyInstituteMap List*/
    $scope.getFacultyInstituteMap = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/FacultyInstituteMap/FacultyInstituteMapGet',
            
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
                    $scope.FacultyInstituteMapTableParams = new NgTableParams({
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
            //data: $scope.FacultyInstituteMap,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.FacultyList = {};
                }
                else {
                    $scope.FacultyList = response.obj;
                }

            })
            .error(function (res) {
               
            });
    };

    /*Get Institute List*/
    $scope.getInstituteList = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstInstitute/MstInstituteGetForDropDown',
            //data: $scope.FacultyInstituteMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.InstituteList = {};
                }
                else {
                    $scope.InstituteList = response.obj;
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Add FacultyInstituteMap*/
    $scope.addFacultyInstituteMap = function () {
         
            $http({
                method: 'POST',
                url: 'api/FacultyInstituteMap/FacultyInstituteMapAdd',
                data: $scope.FacultyInstituteMap,
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
                        $scope.FacultyInstituteMap = {};
                        $scope.getFacultyInstituteMap();
                        
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        
    };

    /*Modify FacultyInstituteMap Data*/
    $scope.modifyFacultyInstituteMapData = function (data) {
          
        $scope.showFormFlag = true;
        $scope.FacultyInstituteMap = data;
        $scope.FacultyGet();
        $scope.MstInstituteGet();       
        $(window).scrollTop(0);  
        };  

    /*Update FacultyInstituteMap*/
    $scope.editFacultyInstituteMap = function () {
        
        $http({
            method: 'POST',
            url: 'api/FacultyInstituteMap/FacultyInstituteMapUpdate',
            data: $scope.FacultyInstituteMap,
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
                    $scope.showFormFlag = false;
                    $scope.getFacultyInstituteMap();
                
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Delete FacultyInstituteMap*/
    $scope.deleteFacultyInstituteMap = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.FacultyInstituteMap = data;

            $http({
                method: 'POST',
                url: 'api/FacultyInstituteMap/FacultyInstituteMapDelete',
                data: $scope.FacultyInstituteMap,
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
                        $scope.getFacultyInstituteMap();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };    

    /*Add New FacultyInstituteMap*/
    $scope.newFacultyInstituteMapAdd = function () {
        $state.go('FacultyInstituteMapAdd');
    };

    /*Back to Edit Page of FacultyInstituteMap*/
    $scope.backToList = function () {
        $state.go('FacultyInstituteMapEdit');
    };

    /*Active Enable FacultyInstituteMap*/
    $scope.ShowFacultyInstituteMap = function (data) {

        $scope.FacultyInstituteMap = data;

        $http({
            method: 'POST',
            url: 'api/FacultyInstituteMap/FacultyInstituteMapIsActive',
            data: $scope.FacultyInstituteMap,
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
                    $scope.getFacultyInstituteMap();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable FacultyInstituteMap*/
    $scope.HideFacultyInstituteMap = function (data) {

        $scope.FacultyInstituteMap = data;

        $http({
            method: 'POST',
            url: 'api/FacultyInstituteMap/FacultyInstituteMapIsSuspended',
            data: $scope.FacultyInstituteMap,
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
                    $scope.getFacultyInstituteMap();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});