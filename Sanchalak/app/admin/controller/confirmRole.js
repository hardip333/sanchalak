app.controller('EmpDashboardCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies,  $mdDialog, NgTableParams) {
    //alert("in");
    var favoriteCookie = $cookies.get('token');

    if (!favoriteCookie) {
        $state.go('login');
    }
    $scope.loginData = {};
    $scope.loginData.token = $cookies.get('token');
    //alert($cookies.get('token'));
    $scope.loginData.id = $cookies.get('id');
    console.log($scope.loginData.token);
    $localStorage.isMenuLoded = false;
    $localStorage.menulist = {};
    // console.log($scope.loginData.id);

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
            //alert("TTT");
            console.log(response.obj);
            $scope.roleInfo = response.obj;
            //new NgTableParams({}, { dataset: response.obj });
        }

    })
        .error(function (res) {
            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
        });

    $scope.userChangeState = function (res) {
        console.log(res);
    }
    $scope.confirmRoleSubmit = function () {
        //localStorage.clear();
        
        //alert("eeeee" + $scope.roleName.Id);
        //$rootScope.Id = $scope.roleName.Id;
        var id = $scope.roleName.Id;
        var typeIdWithFullName = id.split("?");


        var arr = typeIdWithFullName[0].split("_");

        $localStorage.typePrefix = arr[0];// Type like Fac=Faculty, DEP=for department
        
        var format = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/;
        var checkIds = format.test(arr[1]);
        $scope.FacInstDep = arr[1];
        
        if (checkIds) {
            
            if ($localStorage.typePrefix == "DEP") {
                //var getFacInstDepIdForDepartment = checkIds.split("|");
                $scope.DepId = $scope.FacInstDep.split('|')[0];
                $scope.InstId = $scope.FacInstDep.split('|')[1];
                $scope.FacId = $scope.FacInstDep.split('|')[2];
                $cookies.put('facultyDepartIntituteId', $scope.DepId);
                $cookies.put('DepartmentId', $scope.DepId);
                $cookies.put('InstituteId', $scope.InstId);
                $cookies.put('FacultyId', $scope.FacId);
            }
            else if ($localStorage.typePrefix == "INS") {
                $scope.InstId = $scope.FacInstDep.split('|')[0];
                $scope.FacId = $scope.FacInstDep.split('|')[1];
                $cookies.put('facultyDepartIntituteId', $scope.InstId);
                $cookies.put('InstituteId', $scope.InstId);
                $cookies.put('FacultyId', $scope.FacId);
            }
            else
            {
                $scope.FacId = $scope.FacInstDep.split('|')[0];
                $cookies.put('facultyDepartIntituteId', $scope.FacId);
                $cookies.put('FacultyId', $scope.FacId);
            }
        }
        else
        {
            if ($localStorage.typePrefix == "DEP") {
                $scope.DepId = $scope.FacInstDep.split('|')[0];
                $cookies.put('facultyDepartIntituteId', $scope.DepId);
                $cookies.put('DepartmentId', $scope.DepId);
            }
            else if ($localStorage.typePrefix == "INS") {
                $scope.InstId = $scope.FacInstDep.split('|')[0];
                $cookies.put('facultyDepartIntituteId', $scope.InstId);
                $cookies.put('InstituteId', $scope.InstId);
            }
            else {
                $scope.FacId = $scope.FacInstDep.split('|')[0];
                $cookies.put('facultyDepartIntituteId', $scope.FacId);
                $cookies.put('FacultyId', $scope.FacId);
            }
        }

        $localStorage.roleId = arr[2];// Role Id
        $localStorage.localObj = {};
        $localStorage.PreProgInstData = {};
		$localStorage.BacktoPostPage = {}; //Added by Mohini for BacktoPostVerification Faculty Page
		$localStorage.BacktoPostPageAcademic = {}; //Added by Mohini for BacktoPostVerification Academic Page
        $localStorage.BacktoRefundCasePage = {}; //Added by Mohini for Refund Process of Academic Page
        $localStorage.BacktoRefundCaseAuditPage = {}; //Added by Mohini for Refund Process of Audit Page
        $localStorage.BacktoRefundCaseCancelPage = {}; //Added by Mohini for Refund Process Cancel Admission for Academic Page
        $localStorage.BacktoRefundCaseCancelAuditPage = {}; //Added by Mohini for Refund Process Cancel Admission for Audit Page

        var designationNameWithFaculty = typeIdWithFullName[1].split("_");

        $localStorage.designationName = designationNameWithFaculty[0];
        $localStorage.facultyName = designationNameWithFaculty[1];

        // $localStorage.roleId = $scope.roleName.Id;
       // alert($localStorage.roleId + "===" + $scope.roleName.Id);
        $cookies.put('roleId', $localStorage.roleId);
       // $cookies.put('facultyDepartIntituteId', $localStorage.facultyDepartIntituteId);
        $cookies.put('typePrefix', $localStorage.typePrefix);
        //alert($localStorage.designationName + "===" + $localStorage.facultyName);
        $cookies.put('designationName', $localStorage.designationName);
        $cookies.put('facultyName', $localStorage.facultyName);

        $http({
            method: 'POST',
            url: 'api/Role/createTokenForRole',
            data: { Id: arr[2]},
            headers: { "Content-Type": 'application/json' }
        }).success(function (response) {
            $rootScope.showLoading = false;

            if (response.response_code != "200") {
                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
            }
            else {
                $cookies.put("userRoleToken", response.obj);
                //debugger;
                if (arr[2] == 3 || arr[2] == 2 || arr[2] == 8) {
                    $state.go('dashboardadmin');
                }
                else if(arr[2]==16)
                {
                    $state.go('MstCentralAdmissionEdit');
                }
                else if(arr[2]==18)
                {
                    $state.go('RefundCaseByAudit');
                }    
				else if (arr[2] == 5) {
                    $state.go('FEEADMIN');
                }
                else if (arr[2] == 7) {
                    $state.go('dashboardacademic');
                }
                else if (arr[2] == 17) {
                    $state.go('DepartmentReports');
                }
            }
        })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });



        /* $scope.roleName.UserId = $cookies.get('id');
         console.log("Test");
         console.log($scope.roleName);
         $http({
             method: 'POST',
             url: 'api/Role/GetPermissionByUserRole',
             data: $scope.roleName,
             headers: { "Content-Type": 'application/json' }
         }).success(function (response) {
                 $rootScope.showLoading = false;
                 
                 if (response.response_code != "200") {
                     $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                 }
                 else {
                     console.log("asd");
                     
                     $('#sign-in-modal').modal('dispose');
                     $("#sign-in-modal").removeClass('modal-backdrop').modal('hide');
                     console.log(response.obj);
                     $cookies.put("UserRoleId", response.obj.RoleId);
                     //$cookies.put("UserRoleId",response.RoleId);
                     //$('.modal-backdrop').remove();
                    // $state.go('dashboard');
 
                 }
             })
             .error(function (res) {
                 $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
             });*/
    }

	$scope.logoutUserProcess = function () {
        localStorage.clear();
        $localStorage.$reset();
		var cookies = $cookies.getAll();
		angular.forEach(cookies, function (v, k) {
		$cookies.remove(k);
		});
		$state.go('login');
	};

});