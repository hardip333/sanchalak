app.controller('SelectedPaperReportAcademicCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Paper Report for Academic Section";

    $scope.SRBP = {};
    $scope.ShowTableFlag = false;

    $scope.getMstInstitute = function () {
        $scope.ShowTableFlag = false;
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/MstInstituteGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.InstituteList = {};

                    }
                }
                else {
                    $scope.InstituteList = response.obj;

                }
            })
       
            .error(function (res) {
                alert(res);
            });
    };

    $scope.IncAcademicYearListGet = function () {
       
        $scope.ShowTableFlag = false;
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/AcademicYearGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.AcademicList = {};

                    }
                }
                else {
                    $scope.AcademicList = response.obj;
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    /* Programme List Get Method*/
    $scope.ProgrammeGetbyInstId = function () {

        $scope.ShowTableFlag = false;
        $scope.ProgrammeList = {};
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/MstProgrammeGetByInstId',
            data: $scope.SRBP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.ProgrammeList = {};

                    }
                }
                else {
                    $scope.ProgrammeList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.ProgPartInstListGet = function () {
        $scope.ShowTableFlag = false;
        $scope.ProgPartList = {};
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/ProgrammePartListGetByProgrammeId',
            data: $scope.SRBP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.ProgPartList = {};

                    }
                }
                else {
                    $scope.ProgPartList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /* Branch List Get Method*/
    $scope.BranchGet = function () {
        $scope.ShowTableFlag = false;
        $scope.BList = {};
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/MstProgrammeBranchListGetByProgrammePartId',
            data: $scope.SRBP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.BList = {};

                    }
                }
                else {
                    $scope.BList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /* ProgrammePT List Get Method*/
    $scope.PTsGet = function () {
        $scope.ShowTableFlag = false;
        $scope.PTList = {};
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/ProgrammePartTermListGetByProgrammePartId',
            data: $scope.SRBP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.PTList = {};
                        $scope.ShowInstitute = false;
                        $scope.ShowTable = false;
                        alert(response.obj);
                    }
                }
                else {
                    $scope.PTList = response.obj;
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.GetPaperList = function () {
        $scope.ShowTableFlag = false;
        $scope.SRBP.ProgrammeInstancePartTermId =$scope.SRBP.ProgrammeInstancePartTerm.Id;
        $scope.SRBP.InstancePartTermName =$scope.SRBP.ProgrammeInstancePartTerm.InstancePartTermName;
        $scope.PaperList = {};
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/GetPaperListByPTIdandInstId',
            data: $scope.SRBP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.PaperList = {};
                        alert(response.obj);
                    }
                }
                else {
                    $scope.PaperList = response.obj;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    
    $scope.GetStudentList = function () {
        $scope.StudentList = {};
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/GetStudentListByPaperIdandInstId',
            data: $scope.SRBP,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.StudentList = {};
                        $scope.ShowTableFlag = false;
                        alert(response.obj);
                    }
                }
                else {
                    $scope.StudentList = response.obj;
                    $scope.ShowTableFlag = true;
                    if ($.fn.dataTable.isDataTable('#example')) {
                        $('#example').dataTable().fnClearTable();
                        $('#example').DataTable().destroy();

                    }
                    $(document).ready(function () {
                        $('#example').DataTable({
                            "bPaginate": true,
                            "ordering": false,
                            dom: 'Bfrtip',
                            buttons: [
                                { extend: 'excel', title: 'Paper - Student Report', exportOptions: { columns: "thead th:not(.noExport)" } }
                                , { extend: 'pdf', orientation: 'landscape', title: 'Paper - Student Report', exportOptions: { columns: "thead th:not(.noExport)" } }
                                , { extend: 'print', title: 'Paper - Student Report', exportOptions: { columns: "thead th:not(.noExport)" } }

                            ]
                        });
                    });

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

})