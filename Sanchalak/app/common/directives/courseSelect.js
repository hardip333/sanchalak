app.directive("courseSelect", function () {
    return {
        templateUrl: "UI/components/courseSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.CourseGetForDropDownList = [];
            $scope.defaultCourseList = [];
            var courseObj = {
                Id : 0,
                ProgrammeName: "Select Programme"
            }
            $scope.defaultCourseList.push(courseObj);

            $scope.MstProgrammeListGetForDropDown = function () {

                $rootScope.showLoading = true;

                $http({
                    method: 'POST',
                    url: 'api/MstProgramme/MstProgrammeListGetForDropDown',
                    data: {},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;
                        if (response.response_code != "200") {
                      
                            alert(response.obj);
                        }
                        else {
                            $scope.tempList = response.obj;
                            $scope.MstProgrammeListGetForDropDownList = $scope.defaultCourseList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        $rootScope.showLoading = false;
                        alert(res.obj);
                    });
            }
            $scope.MstProgrammeListGetForDropDown();
        }],
        link: function (e, t, a) {
        }
    }
})