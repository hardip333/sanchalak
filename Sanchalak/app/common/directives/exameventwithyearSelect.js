app.directive("exameventwithyearSelect", function () {
    return {
        templateUrl: "UI/components/exameventwithyearSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.ExamMasterListGetActiveList = [];
            $scope.defaultEventsList = [];
            var eventsObj = {
                Id: 0,
                DisplayName : "Select Exam Event"
            }
            $scope.defaultEventsList.push(eventsObj);

            $scope.getExamMasterListGetActive = function (data) {

                $http({
                    method: 'POST',
                  /*  url: 'api/ExamMaster/MstExamGetActive',*/
                    url: 'api/ExamEventMaster/ExamEventMasterListGetActive',
                    data: { AcademicYearId : data},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {

                            $scope.tempList = response.obj;
                            $scope.ExamMasterListGetActiveList = $scope.defaultEventsList.concat($scope.tempList);
                   
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
        /*    $scope.getExamMasterListGetActive();*/

        }],
        link: function (e, t, a) {
        }
    }
})