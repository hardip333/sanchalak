app.directive("examvenuewithscheduleSelect", function () {
    return {
        templateUrl: "UI/components/examvenuewithscheduleSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.attachedCourseVenueMapListforlist = [];
            $scope.defaultVenueWithSchList = [];
            var venueObj = {
                ExamVenueId : 0,
                ExamVenue: "Select Exam Venue"
            }
            $scope.defaultVenueWithSchList.push(venueObj);

            $scope.getActiveAttachedCourseVenueMapList = function (data) {
    
                $http({
                    method: 'POST',
                    url: 'api/CourseAttachVenue/getAttachedCourseVenueMapList',
                    data: data,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                   
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                     
                            alert(response.obj);
                        }
                        else {
                        
                            $scope.tempList = response.obj;
                          
                            $scope.attachedCourseVenueMapListforlist = $scope.defaultVenueWithSchList.concat($scope.tempList);
           

                        }
                    })
                    .error(function (res) {
                   
                        alert(res.obj);
                    });
            }
      /*      $scope.getActiveAttachedCourseVenueMapList();*/


        }],
        link: function (e, t, a) {
        }
    }
})