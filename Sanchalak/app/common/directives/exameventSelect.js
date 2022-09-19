app.directive("exameventSelect", function () {
    return {
        templateUrl: "UI/components/exameventSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.ExamMasterListGetActiveList = [];
            $scope.defaultEventList = [];
            var eventObj = {
                Id: 0,
                DisplayName : "Select Exam Event"
            }
            $scope.defaultEventList.push(eventObj);

            $scope.getExamMasterListGetActive = function () {

                $http({
                    method: 'POST',
          /*          url: 'api/ExamMaster/MstExamGetActive',*/
                    url: 'api/ExamEventMaster/ExamEventMasterListGetActive',
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
                            $scope.ExamMasterListGetActiveList = $scope.defaultEventList.concat($scope.tempList);

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getExamMasterListGetActive();

        }],
        link: function (e, t, a) {
        }
    }
})