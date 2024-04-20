use std::sync::Arc;
use tonic::{transport::Server, Request, Response, Status};

pub mod simple {
    tonic::include_proto!("simple"); // The string specified here must match the proto package name
}

use simple::bar_service_server::{BarService, BarServiceServer};
use crate::simple::{Bar, BarsResponse};


#[derive(Debug)]
pub struct GrpcBarService {
    state: Arc<AppState>,
}

#[derive(Debug)]
struct AppState {
    bar100: BarsResponse,
    bar5000: BarsResponse,
}

impl Default for GrpcBarService {
    fn default() -> Self {
        let state = Arc::new(AppState {
            bar100: BarsResponse::default(),
            bar5000: BarsResponse::default(),
        });

        GrpcBarService { state }
    }
}

#[tonic::async_trait]
impl BarService for GrpcBarService {
    async fn get_bar100(&self, _request: Request<()>) -> Result<Response<BarsResponse>, Status> {
        let bars = self.state.as_ref().bar100.clone();
        Ok(Response::new(bars))
    }

    async fn get_bar5000(&self, _request: Request<()>) -> Result<Response<BarsResponse>, Status> {
        let bars = self.state.as_ref().bar5000.clone();
        Ok(Response::new(bars))
    }
}


#[tokio::main]
async fn main() -> Result<(), Box<dyn std::error::Error>> {
    let addr = "[::1]:3007".parse()?;

    let symbol: &str = "AAPL";

    let bar100: BarsResponse = BarsResponse {
        symbol: symbol.to_string(),
        bars: vec![Bar {
            open: 432.3,
            high: 234.3,
            low: 324.3,
            close: 23.,
        }; 100],
    };

    let bar5000: BarsResponse = BarsResponse {
        symbol: symbol.to_string(),
        bars: vec![Bar {
            open: 432.3,
            high: 234.3,
            low: 324.3,
            close: 23.,
        }; 5000],
    };

    let service = GrpcBarService { state: Arc::new(AppState { bar100, bar5000 }) };

    Server::builder()
        .add_service(BarServiceServer::new(service))
        .serve(addr)
        .await?;

    Ok(())
}