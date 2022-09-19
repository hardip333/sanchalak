app.controller("DepartmentReportsCtrl", function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    var cookie = $cookies.get("token");
    if (!cookie) {
        debugger;
        $cookies.get("token");
        $state.go('login');
    }
    else {
        debugger;
        $cookies.get("token");
    }


    $scope.DepartmentReportParam = {};

    //Show Notification
    $scope.Notification = function (msg) {
        $.growl({
            message: msg
        }, {
            type: 'success',
            allow_dismiss: true,
            label: 'Cancel',
            className: 'btn btn-lg',
            placement: {
                from: 'top',
                align: 'right'
            },
            //delay: 3500,
            animate: {
                enter: 'animated zoomIn',
                exit: 'animated zoomOut'
            },
            offset: {
                x: 30,
                y: 30
            }
        });
    };

    //Get Faculty Name By Id
    $scope.getFacultyById = function () {
        
        var FacultyId = { Id: $cookies.get('FacultyId')};

        $http({
            method: 'POST',
            url: 'api/DepartmentReports/MstFacultyGetbyId',
            data: FacultyId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code != '200') {

                }
                else {
                    
                    $scope.DepartmentReportParam.FacultyName = response.obj[0].FacultyName;
                    $scope.DepartmentReportParam.FacultyId = response.obj[0].Id;
                    $scope.getInstituteById();
                }
                
            })
            .error(function (response) {
                
            });
    };

    //Get Institute Name By Id
    $scope.getInstituteById = function () {

        var InstituteId = { Id: $cookies.get('InstituteId') };

        $http({
            method: 'POST',
            url: 'api/MstInstitute/MstInstituteGetbyId',
            data: InstituteId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code != '200') {

                }
                else {
                    $scope.DepartmentReportParam.InstituteName = response.obj.InstituteName;
                    $scope.DepartmentReportParam.InstituteId = response.obj.Id;
                    $scope.getDepartmentById();
                }

            })
            .error(function (response) {
                
            });
    };

    //Get Department Name By Id
    $scope.getDepartmentById = function () {

        var DepartmentId = { Id: $cookies.get('DepartmentId') };

        $http({
            method: 'POST',
            url: 'api/MstSubject/MstSubjectGetbyId',
            data: DepartmentId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code != '200') {

                }
                else {
                    $scope.DepartmentReportParam.DepartmentName = response.obj.SubjectName;
                    $scope.DepartmentReportParam.DepartmentId = response.obj.Id;
                    $scope.getAcademicYearList();
                }

            })
            .error(function (response) {
                
            });
    };

    //Get Academic Year List
    $scope.getAcademicYearList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
            //data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                if (response.response_code != '200') {

                }
                else {
                    if (response.obj.length == 1) {
                        $scope.AcademicYearList = response.obj;
                        $scope.DepartmentReportParam.AcademicYearId = response.obj[0].Id;
                        $scope.DepartmentReportParam.AcademicYearName = response.obj[0].AcademicYearCode;
						$scope.getProgrammeListByFacInstDepId($scope.DepartmentReportParam.AcademicYearId);
                    }
                    else {
                        $scope.AcademicYearList = response.obj;
                    }
                    
                }

            })
            .error(function (response) {
                
            });
    };

    //Get Programme List By Faculty Institute Department Id
    $scope.getProgrammeListByFacInstDepId = function (id) {
        var FacInstDep = {
            FacultyId: $cookies.get('FacultyId')
            , InstituteId: $cookies.get('InstituteId')
            , SubjectId: $cookies.get('DepartmentId')};
        $http({
            method: 'POST',
            url: 'api/DepartmentReports/MstProgrammeGetByFacInstSubId',
            data: FacInstDep,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                if (response.response_code != '200') {

                }
                else {
                    if (response.obj.length == 1) {
                        $scope.ProgrammeList = response.obj;
                        $scope.DepartmentReportParam.ProgrammeId = response.obj[0].Id;
                        $scope.DepartmentReportParam.ProgrammeName = response.obj[0].ProgrammeName;
						$scope.getProgInstPartTermGetByFacAcProgId($scope.DepartmentReportParam.ProgrammeId);
                    }
                    else {
                        $scope.ProgrammeList = response.obj;
                    }
                    

                }

            })
            .error(function (response) {

            });
    };

    //Get Programme List By Faculty Institute Department Id
    $scope.getProgInstPartTermGetByFacAcProgId = function (id) {
        var FacAcProg = {
            FacultyId: $cookies.get('FacultyId')
            , AcademicYearId: $scope.DepartmentReportParam.AcademicYearId
            , ProgrammeId: id//$scope.DepartmentReportParam.ProgrammeId
        };
        $http({
            method: 'POST',
            url: 'api/DepartmentReports/IncProgInstPartTermGetByFacAcProgId',
            data: FacAcProg,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                if (response.response_code != '200') {

                }
                else {
                    if (response.obj.length == 1) {
                        $scope.IncProgrammeInstancePartTermList = response.obj;
                        $scope.DepartmentReportParam.InstancePartTermId = response.obj[0].Id;
                        $scope.DepartmentReportParam.InstancePartTermName = response.obj[0].InstancePartTermName;
                    }
                    else {
                        $scope.IncProgrammeInstancePartTermList = response.obj;
                    }


                }

            })
            .error(function (response) {

            });
    };

    //Get Student By IncProgrammeInstancePartTermId
    $scope.getStudentByIncProgrammeInstancePartTermId = function (DepartmentReportType) {
        
        $scope.DepartmentReportParam.ReportType = DepartmentReportType;
        if ($scope.DepartmentReportParam.ReportType == 'StudentData') {

            var StuList = {
                AcademicYearId: $scope.DepartmentReportParam.AcademicYearId
                , FacultyId: $cookies.get('FacultyId')
                , InstituteId: $cookies.get('InstituteId')
                , SubjectId: $cookies.get('DepartmentId')
                , InstancePartTermId: $scope.DepartmentReportParam.InstancePartTermId
                , ProgrammeId: $scope.DepartmentReportParam.ProgrammeId
                , ReportType: $scope.DepartmentReportParam.ReportType
            };
            $http({
                method: 'POST',
                url: 'api/DepartmentReports/DepartmentStudentList',
                data: StuList,
                headers: { "Content-Type": 'application/json' }
            })
               .success(function (response) {

                    if (response.response_code != '200') {

                    }
                    else {
                        debugger;
                        $scope.DepartmentStudentListTableParam = new NgTableParams({}, { dataset: response.obj });
                        console.log($scope.DepartmentStudentListTableParam);
                        if (response.obj.length > 0) {
                            $scope.StudentData = true;
                        }
                        else {
                            alert('There is no data found');
                        }
                        
                    }
                })
                .error(function (response) {

                });

            

        }
        else {
            $scope.a = false;
        }
        
    };

    //Get Student By IncProgrammeInstancePartTermId and Subject and Paper
    $scope.getStudentByIncProgrammeInstancePartTermId = function (DepartmentReportType) {

        $scope.DepartmentReportParam.ReportType = DepartmentReportType;
        if ($scope.DepartmentReportParam.ReportType == 'StudentData') {

            var StuList = {
                AcademicYearId: $scope.DepartmentReportParam.AcademicYearId
                , FacultyId: $cookies.get('FacultyId')
                , InstituteId: $cookies.get('InstituteId')
                , SubjectId: $cookies.get('DepartmentId')
                , InstancePartTermId: $scope.DepartmentReportParam.InstancePartTermId
                , ProgrammeId: $scope.DepartmentReportParam.ProgrammeId
                , ReportType: $scope.DepartmentReportParam.ReportType
            };
            $http({
                method: 'POST',
                url: 'api/DepartmentReports/DepartmentStudentList',
                data: StuList,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code != '200') {

                    }
                    else {
                        debugger;
                        $scope.DepartmentStudentListTableParam = new NgTableParams({}, { dataset: response.obj });
                        console.log($scope.DepartmentStudentListTableParam);
                        if (response.obj.length > 0) {
                            $scope.StudentData = true;
                        }
                        else {
                            alert('There is no data found');
                        }
                    }
                })
                .error(function (response) {
                });
        }
        else {
            $scope.a = false;
        }

    };
});

