
app.controller("academicDashboardCtrl", function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    $scope.Ins = {};
    $scope.InstituteId = {};
    $scope.getInstituteById = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Institute = response.obj[0];

                $scope.getTotalProgrammesByInstituteId();
                $scope.getTotalApplicantsByInstituteId();
                $scope.getTotalApplicantsApprovedByInstituteId();
                $scope.getTotalApplicantsFeesPaidByInstituteId();

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getMstProgrammeCountAcademicByInstituteId = function () {

       // var InstId = { InstituteId: $scope.Institute.InstituteId };
        $http({
            method: 'POST',
            url: 'api/DashboardAcademic/MstProgrammeCountAcademicGet',
           // data: InstId,

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.pca = response.obj[0];


            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAdmApplicantsCountAcademicGetByInstituteId = function () {
        $scope.InstituteId = {};
        //var InstId = { InstituteId: $scope.Institute.InstituteId };
        $http({
            method: 'POST',
            url: 'api/DashboardAcademic/AdmApplicantsCountAcademicGet',
            //data: InstId,

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Apca = response.obj[0];


            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAdmApprovedApplicantsCountAcademicByInstituteId = function () {
        //debugger;
        //var InstId = { InstituteId: $scope.Institute.InstituteId };
        $http({
            method: 'POST',
            url: 'api/DashboardAcademic/AdmApprovedApplicantsCountAcademicGet',
            //data: InstId,

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AApca = response.obj[0];

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAdmApplicationFeesPaidCountAcademicByInstituteId = function () {
        //var InstId = { InstituteId: $scope.Institute.InstituteId };
        $http({
            method: 'POST',
            url: 'api/DashboardAcademic/AdmApplicationFeesPaidCountAcademicGet',
            //data: InstId,

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AFpca = response.obj[0];
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.nextbtn = function () {
        $state.go('ProgrammeStatisticsByInstitute');
    };
});

