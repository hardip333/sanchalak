app.directive("parttermforexamfeeSelect", function () {
    return {
        templateUrl: "UI/components/parttermforexamfeeSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.ProgrammePartTermGetByProgrammeIdForCourseList = [];
            $scope.defaultPartTermsforList = [];
            var parttermsObj = {
                Id : 0,
                PartTermName: "Select Programme Part Term"
            }
            $scope.defaultPartTermsforList.push(parttermsObj);

            $scope.getProgrammePartTermGetByProgrammeIdForCourse = function (data) {

                $http({
                    method: 'POST',
                    url: 'api/MstProgrammePartTerm/ProgrammePartTermGetByProgrammeIdForCourse',
                    data: { ProgrammeId:data },
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {

                            $scope.tempList = response.obj;
                            $scope.ProgrammePartTermGetByProgrammeIdForCourseList = $scope.defaultPartTermsforList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
   /*         $scope.getProgrammePartTermGetByProgrammeIdForCourse();*/


        }],
        link: function (e, t, a) {
        }
    }
})