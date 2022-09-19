app.controller('CopyCourseProgrammeDefinationCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Copy Course Programme Defination";

    $scope.resetCopyCourseProgrammeDefination = function () {
        $scope.CopyCourseProgrammeDefination = {};
    }

    ///$localStorage.FinalModel = {};

    $scope.getFacultyById = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/CopyCourseProgrammeDefination/FacultyGetById',
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


    $scope.getProgrammeGetByFacId = function () {
        //debugger
        var ProgObj = { FacultyId: $scope.CopyCourseProgrammeDefination.FacultyId }
        //alert(ProgObj);
        $http({
            method: 'POST',
            url: 'api/CopyCourseProgrammeDefination/ProgrammeGetByFacId',
            data: ProgObj,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.ProgList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getSpecialisationGetByFacultyId = function (FacultyId, ProgrammeId) {
        //debugger
        $http({
            method: 'POST',
            url: 'api/CopyCourseProgrammeDefination/SpecialisationGetByFacultyId',
            data: {
                FacultyId: $scope.CopyCourseProgrammeDefination.FacultyId,
                ProgrammeId: $scope.CopyCourseProgrammeDefination.ProgrammeId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.SpecList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getCopyCourseProgrammeDefination = function (FacultyId, ProgrammeId, SpecialisationId) {
        //debugger
        $http({
            method: 'POST',
            url: 'api/CopyCourseProgrammeDefination/CopyCourseProgrammeDefinationGet',
            data: {
                FacultyId: $scope.CopyCourseProgrammeDefination.FacultyId,
                ProgrammeId: $scope.CopyCourseProgrammeDefination.ProgrammeId,
                SpecialisationId: $scope.CopyCourseProgrammeDefination.SpecialisationId
            },
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
                    console.log(response.obj);
                    if (response.obj === "Record Not Found") {

                        $scope.NoRecordFound = true;
                        $scope.CopyCourseProgrammeDefinationTableParams = new NgTableParams({
                        }, {
                            dataset: [],
                        });
                    }
                    else {
                        $scope.CopyCourseProgrammeDefinationTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        $scope.DatewiseCenterwisePaperReportData = response.obj[0];
                        $scope.ExcelDatewiseCenterwisePaperReport = response.obj;
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.getSourceAcademicYear = function () {
        //debugger
        $http({
            method: 'POST',
            url: 'api/CopyCourseProgrammeDefination/SourceAcademicYearGet',
            data: $scope.CopyCourseProgrammeDefination,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.AcademicYearList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getDestinationAcademicYear = function () {
        //debugger
        $http({
            method: 'POST',
            url: 'api/CopyCourseProgrammeDefination/DestinationAcademicYearGet',
            data: $scope.CopyCourseProgrammeDefination,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                //debugger
                $scope.AYList = response.obj;
                // $scope.Faculty = response.obj; // Krunal's code               

            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.AddCopyCourseDefination = function (CopyCourseProgrammeDefination1) {
        
        CopyCourseProgrammeDefination1.SpecialisationId = $scope.CopyCourseProgrammeDefination.SpecialisationId;

            $http({
                method: 'POST',
                url: 'api/CopyCourseProgrammeDefination/CopyCourseDefinationAdd',
                data: CopyCourseProgrammeDefination1,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        //debugger
                        alert(response.obj);
                        $scope.getCopyCourseProgrammeDefination();

                        //$state.go('CopyCourseDefination');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        


        




    }


    
    
    
    
})