﻿app.directive("coursewithfacultySelect", function () {
    return {
        templateUrl: "UI/components/coursewithfacultySelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.MstProgrammeGetByFacultyIdList = [];
            $scope.defaultCourseFacultyList = [];
            var facultyObj = {
                Id : 0,
                ProgrammeName: "Select Programme"
            }
            $scope.defaultCourseFacultyList.push(facultyObj);

            $scope.getMstProgrammeGetByFacultyId = function (data) {

                $rootScope.showLoading = true;

                $http({
                    method: 'POST',
                    url: 'api/MstProgramme/MstProgrammeGetByFacultyIdForSchedule',
                    data: { FacultyId : data },
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;
                        if (response.response_code != "200") {
                      
                            alert(response.obj);
                        }
                        else {
                            $scope.tempList = response.obj;
                            $scope.MstProgrammeGetByFacultyIdList = $scope.defaultCourseFacultyList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        $rootScope.showLoading = false;
                        alert(res.obj);
                    });
            }
         /*   $scope.MstProgrammeListGetForDropDown();*/
        }],
        link: function (e, t, a) {
        }
    }
})