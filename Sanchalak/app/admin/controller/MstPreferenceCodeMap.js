app.controller('MstPreferenceCodeMapCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Manage MstPreference Code Map";

    $scope.resetMstPreferenceCodeMap = function () {
        $scope.MstPreferenceCodeMap = {};
    };

    $scope.getMstPreferenceCodeMap = function () {
        //debugger
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstPreferenceCodeMap/MstPreferenceCodeMapGet',
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
                    $scope.MstPreferenceCodeMapTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    //$scope.MstPreferenceCodeMapGet();

    $scope.resetUser = function () {
        $scope.user = {};
    };

    $scope.getFacultyById = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/MstPreferenceCodeMap/FacultyGetById',
            data: { Id: $cookies.get('InstituteId') },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.FacultyList = response.obj;

                // $scope.Faculty = response.obj; // Krunal's code               



            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicYear = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/MstPreferenceCodeMap/AcademicYearGetForDropDown',
            data: $scope.MstPreferenceCodeMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.AcadList = response.obj;

                // $scope.Faculty = response.obj; // Krunal's code               



            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.getMstPreferenceGroup = function () {

        $http({
            method: 'POST',
            url: 'api/MstPreferenceCodeMap/MstPreferenceGroupGet',
            data: { FacultyId: $scope.MstPreferenceCodeMap.FacultyId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.PrefList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getMstGroupCode = function () {

        $http({
            method: 'POST',
            url: 'api/MstPreferenceCodeMap/MstGroupCodeGet',
            data: $scope.MstPreferenceCodeMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.CodeList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgInstPartTerm = function () {
        //debugger

        $http({
            method: 'POST',
            url: 'api/MstPreferenceCodeMap/IncProgrammeInstancePartTermGet',
            data: {
                //PreferenceId: $scope.MstPreferenceCodeMap.PreferenceId,
                FacultyId: $scope.MstPreferenceCodeMap.FacultyId,
                AcademicYearId: $scope.MstPreferenceCodeMap.AcademicYearId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger
                $scope.ProgInstPartTermList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };



    $scope.MstPreferenceCodeMapAdd = function () {

        //if ($scope.MstPreferenceCodeMap.GroupName === null || $scope.MstPreferenceCodeMap.GroupName === undefined ||
        //    $scope.MstPreferenceCodeMap.GroupCode === null || $scope.MstPreferenceCodeMap.GroupCode === undefined ||
        //    $scope.MstPreferenceCodeMap.InstancePartTermName === null || $scope.MstPreferenceCodeMap.InstancePartTermName === undefined)
        //{
        //    alert("Enter All Fields");
        //}
        //else {
        debugger
        $http({
            method: 'POST',
            url: 'api/MstPreferenceCodeMap/MstPreferenceCodeMapAdd',
            data: $scope.MstPreferenceCodeMap,
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
                    //$scope.MstPreferenceCodeMap = {};
                    $scope.getMstPreferenceCodeMap();
                    $state.go('MstPreferenceCodeMapEdit');

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
        //}
    }

    $scope.modifyMstPreferenceCodeMap = function (data) {

        $scope.showFormFlag = true;
        $scope.MstPreferenceCodeMap = data;
        $scope.getMstPreferenceGroup();
        $scope.getMstGroupCode();
        $scope.getProgInstPartTerm();
        $scope.getFacultyById();
        $scope.getAcademicYear();
    };

    $scope.MstPreferenceCodeMapEdit = function () {
        alert("Update Data");

        $http({
            method: 'POST',
            url: 'api/MstPreferenceCodeMap/MstPreferenceCodeMapUpdate',
            data: $scope.MstPreferenceCodeMap,
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
                    $scope.getMstPreferenceCodeMap();

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.MstPreferenceCodeMapDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstPreferenceCodeMap = data;

            $http({
                method: 'POST',
                url: 'api/MstPreferenceCodeMap/MstPreferenceCodeMapDelete',
                data: $scope.MstPreferenceCodeMap,
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
                        $scope.getMstPreferenceCodeMap();
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

    $scope.MstPreferenceCodeMapCancle = function () {

        $scope.MstPreferenceCodeMap = {};
        $scope.modifyMstPreferenceCodeMapFlag = false;
    };

    $scope.newMstPreferenceCodeMapAdd = function () {
        $state.go('MstPreferenceCodeMapAdd');
    };

    $scope.backToList = function () {
        $state.go('MstPreferenceCodeMapEdit');
    };

    $scope.displayMstPreferenceCodeMap = function (data) {
        $scope.MstPreferenceCodeMap = data;
    };

    $scope.showUser = function (data) {

        $scope.MstPreferenceCodeMap = data;

        $http({
            method: 'POST',
            url: 'api/MstPreferenceCodeMap/MstPreferenceCodeMapIsActive',
            data: $scope.MstPreferenceCodeMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getMstPreferenceCodeMap();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.HideUser = function (data) {

        $scope.MstPreferenceCodeMap = data;

        $http({
            method: 'POST',
            url: 'api/MstPreferenceCodeMap/MstPreferenceCodeMapIsSuspended',
            data: $scope.MstPreferenceCodeMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getMstPreferenceCodeMap();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});