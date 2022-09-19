app.directive("examcenterSelect", function () {
    return {
        templateUrl: "UI/components/examcenterSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.ExamCenterListGetActiveList = [];
            $scope.defaultCenterList = [];
            var centerObj = {
                Id: 0,
                DisplayName: "Select Exam Center"
            }
            $scope.defaultCenterList.push(centerObj);

            $scope.getExamCenterListGetActive = function () {

                $http({
                    method: 'POST',
                    url: 'api/ExamCenter/ExamCenterListGetActive',
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
                            $scope.ExamCenterListGetActiveList = $scope.defaultCenterList.concat($scope.tempList);

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getExamCenterListGetActive();


        }],
        link: function (e, t, a) {
        }
    }
})