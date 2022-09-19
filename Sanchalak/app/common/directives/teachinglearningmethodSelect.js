app.directive("teachinglearningmethodSelect", function () {
    return {
        templateUrl: "UI/components/teachinglearningmethodSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.MstTeachingLearningMethodGetForDropDownList = [];
            $scope.defaultTeachingList = [];
            var teachObj = {
                Id: 0,
                TeachingLearningMethodName: "Select Teaching Laerning Method"
            }
            $scope.defaultTeachingList.push(teachObj);

            $scope.getMstTeachingLearningMethodGetForDropDown = function () {
                
                $http({
                    method: 'POST',
                    url: 'api/MstTeachingLearningMethod/TeachingLearningGetForDropDownByPartTermId',
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
                            $scope.MstTeachingLearningMethodGetForDropDownList = $scope.defaultTeachingList.concat($scope.tempList);
                   
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getMstTeachingLearningMethodGetForDropDown();

        }],
        link: function (e, t, a) {
        }
    }
})