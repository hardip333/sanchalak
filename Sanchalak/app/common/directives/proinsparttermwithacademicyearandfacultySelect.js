app.directive("proinsparttermwithacademicyearandfacultySelect", function () {
    return {
        templateUrl: "UI/components/proinsparttermwithacademicyearandfacultySelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.activeProgrammeInstancePartTermGetByFacIdAndAcadId = [];
            $scope.defaultPartTermWithFacultyList = [];
            var partfacultyObj = {
                Id: 0,
                InstancePartTermName: "Select Programme Instance Part Term"
            }
            $scope.defaultPartTermWithFacultyList.push(partfacultyObj);

            $scope.getActiveProgrammeInstancePartTermGetByFacIdAndAcadId = function (data) {
           
                if (data.FacultyId !== null && data.AcademicYearId !== null) {
                    $http({
                        method: 'POST',
                        url: 'api/LaunchProgrammePartTerm/ProgrammeInstancePartTermGetByFacIdAndAcadId',
                        data: { FacultyId: data.FacultyId, AcademicYearId: data.AcademicYearId },
                        headers: { "Content-Type": 'application/json' }
                    })
                        .success(function (response) {
                            $rootScope.showLoading = false;

                            if (response.response_code != "200") {
                                alert(response.obj);
                            }
                            else {

                                $scope.tempList = response.obj;
                                $scope.activeProgrammeInstancePartTermGetByFacIdAndAcadId = $scope.defaultPartTermWithFacultyList.concat($scope.tempList);
                            }
                        })
                        .error(function (res) {
                            alert(res.obj);
                        });
                }
                /*            $scope.getActiveProgrammeInstancePartTermGetByFacIdAndAcadId();*/

            }
        }],
        link: function (e, t, a) {
        }
    }
})