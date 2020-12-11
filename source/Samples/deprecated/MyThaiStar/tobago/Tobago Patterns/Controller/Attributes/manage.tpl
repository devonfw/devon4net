        /// <summary>
        /// Post the $Attribute.Classifier.Name$.
        /// </summary>
        /// <param name="$Attribute.Classifier.Name.Camel$View">The model of the $Attribute.Classifier.Name.Camel$ to be processed on the server side.</param>
        /// <returns></returns>
        /// <response code="201">Ok. Created.</response>
        /// <response code="400">Bad Request.</response>    
        /// <response code="401">Unathorized.</response>  
        /// <response code="403">Forbidden. Authorization error.</response>    
        /// <response code="500">Internal Server Error. The search process ended with error.</response>    
        [HttpPost]
        [ActionName("$Attribute.Classifier.Name$")]
        public HttpResponseMessage $Attribute.Classifier.Name$($Attribute.Classifier.Name$View $Attribute.Classifier.Name.Camel$View)
        {
            var $Attribute.Classifier.Name.Camel$Repository = new $Attribute.Classifier.Name$Repository();
            try
            {
                // Add more code here

                $Attribute.Classifier.Name.Camel$Repository.Create($Attribute.Classifier.Name.Camel$View);

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Method to delete the $Attribute.Classifier.Name$.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204"> Ok, no content.</response>
        /// <response code="400">Bad Request.</response>    
        /// <response code="401">Unathorized.</response>     
        /// <response code="403">Forbidden. Authorization error.</response>    
        /// <response code="500">Internal Server Error. The search process ended with error.</response>    
        [HttpDelete]
        [ActionName("Delete")]
        public HttpResponseMessage Delete(long id)
        {
            var $Attribute.Classifier.Name.Camel$Repository = new $Attribute.Classifier.Name$Repository();
            try
            {
                $Attribute.Classifier.Name.Camel$Repository.Delete(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }