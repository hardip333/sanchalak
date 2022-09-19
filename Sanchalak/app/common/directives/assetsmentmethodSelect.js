app.directive("assetsmentmethodSelect", function () {
    return {
        templateUrl: "UI/components/assetsmentmethodSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.MstAssessmentMethodGetForDropDownList = [];
            $scope.defaultassetsList = [];
            var methodObj = {
                Id: 0,
                AssessmentMethodName: "Select Assetsment Method"
            }
            $scope.defaultassetsList.push(methodObj);

            $scope.getMstAssessmentMethodGetForDropDown = function () {

                $http({
                    method: 'POST',
          /*          url: 'api/ExamMaster/MstExamGetActive',*/
                    url: 'api/MstAssessmentMethod/MstAssessmentMethodGetForDropDown',
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
                            $scope.MstAssessmentMethodGetForDropDownList = $scope.defaultassetsList.concat($scope.tempList);

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getMstAssessmentMethodGetForDropDown();

        }],
        link: function (e, t, a) {
        }
    }
})